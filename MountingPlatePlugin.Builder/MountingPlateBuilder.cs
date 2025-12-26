using System;
using System.Collections.Generic;
using Teigha.Runtime;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using HostMgd.ApplicationServices;
using HostMgd.EditorInput;
using MountingPlatePlugin.Model;

namespace MountingPlatePlugin.Builder
{
    public static class MountingPlateBuilder
    {
        public static MountingPlateParameters CurrentParameters { get; set; }
        
        [CommandMethod("CREATE_PLATE")]
        public static void CreatePlate()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
            {
                Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Нет активного документа!");
                return;
            }
            
            Editor ed = doc.Editor;
            ed.WriteMessage("\nСоздание пластины с отверстиями...");
            
            // Параметры по умолчанию
            CurrentParameters = new MountingPlateParameters();
            CurrentParameters.Length = 200;
            CurrentParameters.Width = 100;
            CurrentParameters.Thickness = 15; // Увеличим толщину для лучшей видимости
            CurrentParameters.HolesLength = 4;
            CurrentParameters.HolesWidth = 3;
            
            BuildPlate();
        }
        
        public static void BuildPlate()
        {
            if (CurrentParameters == null) return;
            
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null) return;
            
            Database db = doc.Database;
            Editor ed = doc.Editor;
            
            ed.WriteMessage($"\nПараметры: {CurrentParameters.Length}×{CurrentParameters.Width}×{CurrentParameters.Thickness} мм");
            ed.WriteMessage($"\nОтверстия: {CurrentParameters.HolesLength}×{CurrentParameters.HolesWidth}");
            ed.WriteMessage($"\nДиаметр отверстий: {CurrentParameters.HoleDiameter:F1} мм");
            
            try
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    var blockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    var modelSpace = tr.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    
                    // 1. Создаем основную пластину (прямоугольник)
                    var plate = CreatePlateSolid(CurrentParameters);
                    
                    // Центрируем
                    plate.TransformBy(Matrix3d.Displacement(new Vector3d(0, 0, CurrentParameters.Thickness/2)));
                    
                    // Добавляем в модель
                    modelSpace.AppendEntity(plate);
                    tr.AddNewlyCreatedDBObject(plate, true);
                    
                    // 2. Создаем отверстия
                    if (CurrentParameters.HolesLength > 0 && CurrentParameters.HolesWidth > 0)
                    {
                        CreateHolesNew(tr, modelSpace, plate, CurrentParameters, ed);
                    }
                    
                    tr.Commit();
                    ed.WriteMessage("\n✅ Пластина создана!");
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\n❌ Ошибка: {ex.Message}");
            }
        }
        
        private static Solid3d CreatePlateSolid(MountingPlateParameters parameters)
        {
            // Создаем пластину через эскиз и выдавливание
            // 1. Создаем прямоугольный контур
            double halfLength = parameters.Length / 2;
            double halfWidth = parameters.Width / 2;
            
            Point3d p1 = new Point3d(-halfLength, -halfWidth, 0);
            Point3d p2 = new Point3d(halfLength, -halfWidth, 0);
            Point3d p3 = new Point3d(halfLength, halfWidth, 0);
            Point3d p4 = new Point3d(-halfLength, halfWidth, 0);
            
            // Создаем линии
            Line line1 = new Line(p1, p2);
            Line line2 = new Line(p2, p3);
            Line line3 = new Line(p3, p4);
            Line line4 = new Line(p4, p1);
            
            // Создаем коллекцию кривых
            var curves = new DBObjectCollection();
            curves.Add(line1);
            curves.Add(line2);
            curves.Add(line3);
            curves.Add(line4);
            
            // Создаем Region
            var regions = Region.CreateFromCurves(curves);
            Region plateRegion = regions.Count > 0 ? regions[0] as Region : null;
            
            // Создаем Solid3d через выдавливание
            var plate = new Solid3d();
            if (plateRegion != null)
            {
                plate.Extrude(plateRegion, parameters.Thickness, 0);
                
                // Очищаем
                plateRegion.Dispose();
            }
            else
            {
                // Резервный вариант
                plate.CreateBox(parameters.Length, parameters.Width, parameters.Thickness);
            }
            
            // Очищаем линии
            line1.Dispose();
            line2.Dispose();
            line3.Dispose();
            line4.Dispose();
            
            return plate;
        }
        
        private static void CreateHolesNew(Transaction tr, BlockTableRecord modelSpace, Solid3d plate, 
            MountingPlateParameters parameters, Editor ed)
        {
            double holeRadius = parameters.HoleDiameter / 2;
            
            // Рассчитываем шаг
            double spacingX = parameters.Length / (parameters.HolesLength + 1);
            double spacingY = parameters.Width / (parameters.HolesWidth + 1);
            
            double startX = -parameters.Length / 2 + spacingX;
            double startY = -parameters.Width / 2 + spacingY;
            
            int holesCreated = 0;
            
            for (int i = 0; i < parameters.HolesLength; i++)
            {
                for (int j = 0; j < parameters.HolesWidth; j++)
                {
                    try
                    {
                        double x = startX + i * spacingX;
                        double y = startY + j * spacingY;
                        
                        // Создаем цилиндр для отверстия
                        var hole = CreateCylinderSolid(new Point3d(x, y, 0), holeRadius, parameters.Thickness + 2);
                        
                        // Позиционируем
                        hole.TransformBy(Matrix3d.Displacement(new Vector3d(0, 0, parameters.Thickness/2)));
                        
                        // Добавляем в модель
                        modelSpace.AppendEntity(hole);
                        tr.AddNewlyCreatedDBObject(hole, true);
                        
                        // Вычитаем из пластины
                        plate.BooleanOperation(BooleanOperationType.BoolSubtract, hole);
                        
                        holesCreated++;
                    }
                    catch (System.Exception ex)
                    {
                        ed.WriteMessage($"\n⚠️ Ошибка отверстия [{i},{j}]: {ex.Message}");
                    }
                }
            }
            
            ed.WriteMessage($"\nСоздано отверстий: {holesCreated}");
        }
        
        private static Solid3d CreateCylinderSolid(Point3d center, double radius, double height)
        {
            // Создаем круг
            var circle = new Circle(center, Vector3d.ZAxis, radius);
            
            // Создаем коллекцию кривых
            var curves = new DBObjectCollection();
            curves.Add(circle);
            
            // Создаем Region из круга
            var regions = Region.CreateFromCurves(curves);
            Region circleRegion = regions.Count > 0 ? regions[0] as Region : null;
            
            // Создаем цилиндр выдавливанием
            var cylinder = new Solid3d();
            if (circleRegion != null)
            {
                cylinder.Extrude(circleRegion, height, 0);
                circleRegion.Dispose();
            }
            else
            {
                // Резервный вариант
                cylinder.CreateFrustum(height, radius, radius, 0);
            }
            
            circle.Dispose();
            return cylinder;
        }
    }
}



