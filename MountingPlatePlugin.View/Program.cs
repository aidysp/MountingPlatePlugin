using System;
using Teigha.Runtime;
using HostMgd.ApplicationServices;
using MountingPlatePlugin.Builder;

namespace MountingPlatePlugin.View
{
    public class Program
    {
        [CommandMethod("CREATE_PLATE_FORM")]
        public static void CreatePlateForm()
        {
            try
            {
                var doc = HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                {
                    HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Нет активного документа!");
                    return;
                }
                
                // Показываем форму
                var form = new MainForm();
                var result = HostMgd.ApplicationServices.Application.ShowModalDialog(form);
                
                // Если форма закрыта с OK
                if (result.ToString() == "OK")
                {
                    // Проверяем параметры (они уже должны быть сохранены в Builder)
                    if (MountingPlateBuilder.CurrentParameters != null)
                    {
                        // Строим пластину
                        doc.Editor.WriteMessage("\nПостроение пластины с отверстиями...");
                        MountingPlateBuilder.BuildPlate();
                    }
                    else
                    {
                        doc.Editor.WriteMessage("\nПараметры не установлены!");
                    }
                }
            }
            catch (System.Exception ex)
            {
                var doc = HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                if (doc != null)
                {
                    doc.Editor.WriteMessage("\nОшибка: " + ex.Message);
                }
            }
        }
    }
}
