using System;

namespace MountingPlatePlugin.Model
{
    class TestProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Тест модели монтажной пластины ===\n");

            var plate = new MountingPlateParameters();

            Console.WriteLine($"Длина: {plate.Length} мм");
            Console.WriteLine($"Ширина: {plate.Width} мм");
            Console.WriteLine($"Толщина: {plate.Thickness} мм");
            Console.WriteLine($"Отверстий по длине: {plate.HolesLength}");
            Console.WriteLine($"Отверстий по ширине: {plate.HolesWidth}");
            Console.WriteLine($"Диаметр отверстий: {plate.HoleDiameter:F1} мм");
            Console.WriteLine($"Всего отверстий: {plate.TotalHoles}");
            Console.WriteLine($"Отступ от края: {plate.EdgeOffset:F1} мм");

            // Тест валидации
            Console.WriteLine($"\nВалидация всех параметров: {plate.ValidateAll()}");

            // Тест изменения параметра
            try
            {
                plate.Length = 300;
                Console.WriteLine($"Новая длина: {plate.Length} мм");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\n=== Тест завершен ===");
            Console.ReadKey();
        }
    }
}