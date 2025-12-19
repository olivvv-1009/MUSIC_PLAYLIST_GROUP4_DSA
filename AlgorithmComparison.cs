using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CTDL
{
    public class AlgorithmComparison
    {
        // Cấu hình Sắp xếp 
        public static int N_LOOPS_SORT = 2000;
        public static int DATA_COUNT_SORT = 1000;

        // Cấu hình Tìm kiếm 
        public static int N_LOOPS_SEARCH = 20000;
        public static int DATA_COUNT_SEARCH = 5000;


        /// Đo lường hiệu năng Sắp xếp: Lặp N lần, lấy tổng thời gian và bộ nhớ đỉnh.
        private void MeasureSortPerformance(Action<List<string>> sortFunc, List<string> originalData, int loops, out long memoryMax, out long memoryBefore)
        {
            GC.Collect();
            memoryBefore = GC.GetTotalMemory(false);
            memoryMax = memoryBefore;

            for (int t = 0; t < loops; t++)
            {
                // 1. Tạo bản sao dữ liệu mới cho mỗi lần lặp 
                List<string> currentArr = new List<string>(originalData);

                // 2. Đo bộ nhớ đỉnh ngay trước khi chạy thuật toán
                long x = GC.GetTotalMemory(false);
                if (x > memoryMax) memoryMax = x;

                // 3. Thực thi thuật toán
                sortFunc(currentArr);
            }
        }

        /// Đo lường hiệu năng Tìm kiếm: Lặp N lần, lấy tổng thời gian và bộ nhớ đỉnh.
        private void MeasureSearchPerformance(Func<int> searchFunc, int loops, out long memoryMax, out long memoryBefore)
        {
            GC.Collect();
            memoryBefore = GC.GetTotalMemory(false);
            memoryMax = memoryBefore;

            for (int t = 0; t < loops; t++)
            {
                // 1. Đo bộ nhớ đỉnh ngay trước khi chạy thuật toán
                long x = GC.GetTotalMemory(false);
                if (x > memoryMax) memoryMax = x;

                // 2. Thực thi thuật toán
                searchFunc();
            }
        }

        public void RunComparison()
        {
            Timing timer = new Timing();
            StringBuilder report = new StringBuilder();

            // Dữ liệu ban đầu
            List<string> data_sort = GenerateDummyData(DATA_COUNT_SORT);
            List<string> data_search = GenerateDummyData(DATA_COUNT_SEARCH);

            report.AppendLine("BÁO CÁO SO SÁNH HIỆU NĂNG ƯU TIÊN TỐC ĐỘ");
            report.AppendLine($"Thời gian thực hiện: {DateTime.Now}");

            long memoryBefore, memoryMax;

            // --- PHẦN 1: SẮP XẾP ---
            report.AppendLine("\nI. THUẬT TOÁN SẮP XẾP (O(N^2))");
            report.AppendLine($"(Dữ liệu N={DATA_COUNT_SORT}, Lặp={N_LOOPS_SORT})");

            // 1. Bubble Sort
            timer.startTime();
            MeasureSortPerformance(BubbleSort, data_sort, N_LOOPS_SORT, out memoryMax, out memoryBefore);
            timer.StopTime();

            report.AppendLine($"- Bubble Sort:");
            report.AppendLine($"  + Time/Loop: {timer.Result().TotalMilliseconds / N_LOOPS_SORT:N6} ms");
            report.AppendLine($"  + Max Mem/Loop: {(memoryMax - memoryBefore) / N_LOOPS_SORT} Bytes");

            // 2. Insertion Sort
            timer.startTime();
            MeasureSortPerformance(InsertionSort, data_sort, N_LOOPS_SORT, out memoryMax, out memoryBefore);
            timer.StopTime();

            report.AppendLine($"- Insertion Sort:");
            report.AppendLine($"  + Time/Loop: {timer.Result().TotalMilliseconds / N_LOOPS_SORT:N6} ms");
            report.AppendLine($"  + Max Mem/Loop: {(memoryMax - memoryBefore) / N_LOOPS_SORT} Bytes");

            // 3. Selection Sort
            timer.startTime();
            MeasureSortPerformance(SelectionSort, data_sort, N_LOOPS_SORT, out memoryMax, out memoryBefore);
            timer.StopTime();

            report.AppendLine($"- Selection Sort:");
            report.AppendLine($"  + Time/Loop: {timer.Result().TotalMilliseconds / N_LOOPS_SORT:N6} ms");
            report.AppendLine($"  + Max Mem/Loop: {(memoryMax - memoryBefore) / N_LOOPS_SORT} Bytes");


            // --- PHẦN 2: TÌM KIẾM ---
            report.AppendLine("\nII. THUẬT TOÁN TÌM KIẾM (O(N) & O(logN))");
            report.AppendLine($"(Dữ liệu N={DATA_COUNT_SEARCH}, Lặp={N_LOOPS_SEARCH})");

            List<string> sortedList = new List<string>(data_search);
            sortedList.Sort(); // Sắp xếp trước
            string keyword = "NonExistentKey_" + DateTime.Now.Ticks;

            // 1. Sequential Search
            timer.startTime();
            MeasureSearchPerformance(() => SequentialSearch(data_search, keyword), N_LOOPS_SEARCH, out memoryMax, out memoryBefore);
            timer.StopTime();

            report.AppendLine($"- Sequential Search:");
            report.AppendLine($"  + Time/Loop: {timer.Result().TotalMilliseconds / N_LOOPS_SEARCH:N6} ms");
            report.AppendLine($"  + Max Mem/Loop: {(memoryMax - memoryBefore) / N_LOOPS_SEARCH} Bytes");

            // 2. Binary Search (Iterative)
            timer.startTime();
            MeasureSearchPerformance(() => BinarySearch(sortedList, keyword), N_LOOPS_SEARCH, out memoryMax, out memoryBefore);
            timer.StopTime();

            report.AppendLine($"- Binary Search (Iter):");
            report.AppendLine($"  + Time/Loop: {timer.Result().TotalMilliseconds / N_LOOPS_SEARCH:N6} ms");
            report.AppendLine($"  + Max Mem/Loop: {(memoryMax - memoryBefore) / N_LOOPS_SEARCH} Bytes");

            // 3. Binary Search Rec (Recursive)
            timer.startTime();
            MeasureSearchPerformance(() => BinarySearchRecursive(sortedList, keyword, 0, sortedList.Count - 1), N_LOOPS_SEARCH, out memoryMax, out memoryBefore);
            timer.StopTime();

            report.AppendLine($"- Binary Search (Rec):");
            report.AppendLine($"  + Time/Loop: {timer.Result().TotalMilliseconds / N_LOOPS_SEARCH:N6} ms");
            report.AppendLine($"  + Max Mem/Loop: {(memoryMax - memoryBefore) / N_LOOPS_SEARCH} Bytes");

            // --- XUẤT FILE ---
            string path = "KetQuaTest_Custom_Optimal.txt";
            File.WriteAllText(path, report.ToString());
            try { Process.Start("notepad.exe", path); } catch { }
        }

        private List<string> GenerateDummyData(int count)
        {
            List<string> dummy = new List<string>();
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                dummy.Add("Bai Hat " + rnd.Next(10000, 99999));
            }
            return dummy;
        }

        void BubbleSort(List<string> arr)
        {
            for (int i = 0; i < arr.Count - 1; i++)
                for (int j = 0; j < arr.Count - i - 1; j++)
                    if (string.Compare(arr[j], arr[j + 1]) > 0)
                    { string temp = arr[j]; arr[j] = arr[j + 1]; arr[j + 1] = temp; }
        }

        void InsertionSort(List<string> arr)
        {
            for (int i = 1; i < arr.Count; i++)
            {
                string key = arr[i]; int j = i - 1;
                while (j >= 0 && string.Compare(arr[j], key) > 0)
                { arr[j + 1] = arr[j]; j--; }
                arr[j + 1] = key;
            }
        }

        void SelectionSort(List<string> arr)
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                int min_idx = i;
                for (int j = i + 1; j < arr.Count; j++)
                    if (string.Compare(arr[j], arr[min_idx]) < 0) min_idx = j;
                string temp = arr[min_idx]; arr[min_idx] = arr[i]; arr[i] = temp;
            }
        }

        int SequentialSearch(List<string> arr, string key)
        {
            for (int i = 0; i < arr.Count; i++) if (arr[i] == key) return i;
            return -1;
        }

        int BinarySearch(List<string> arr, string key)
        {
            int left = 0, right = arr.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int res = string.Compare(arr[mid], key);
                if (res == 0) return mid;
                if (res < 0) left = mid + 1; else right = mid - 1;
            }
            return -1;
        }

        int BinarySearchRecursive(List<string> arr, string key, int left, int right)
        {
            if (left <= right)
            {
                int mid = left + (right - left) / 2;
                int res = string.Compare(arr[mid], key);
                if (res == 0) return mid;
                if (res < 0) return BinarySearchRecursive(arr, key, mid + 1, right);
                else return BinarySearchRecursive(arr, key, left, mid - 1);
            }
            return -1;
        }
    }

    public class Timing
    {
        TimeSpan startingTime; TimeSpan duration;
        public Timing() 
        { 
            startingTime = new TimeSpan(0); 
            duration = new TimeSpan(0); 
        }
        public void StopTime() 
        { 
            duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startingTime); 
        }
        public void startTime() 
        { 
            GC.Collect(); GC.WaitForPendingFinalizers(); 
            startingTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime; 
        }
        public TimeSpan Result() { return duration; }
    }
}