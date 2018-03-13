using System;
using System.Linq;

namespace Loria.Modules.Tasks
{
    public static class DaysOfWeek
    {
        public static int[] All = new[] { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };
        public static int Monday = 1;
        public static int Tuesday = 2;
        public static int Wednesday = 3;
        public static int Thursday = 4;
        public static int Friday = 5;
        public static int Saturday = 6;
        public static int Sunday = 7;
    }

    public static class DaysOfMonth
    {
        public static int[] All = Enumerable.Range(1, 31).ToArray();
    }

    public static class MonthsOfYear
    {
        public static int[] All = new[] { January, February, March, April, May, June, July, August, September, October, November, December };
        public static int January = 1;
        public static int February = 2;
        public static int March = 3;
        public static int April = 4;
        public static int May = 5;
        public static int June = 6;
        public static int July = 7;
        public static int August = 8;
        public static int September = 9;
        public static int October = 10;
        public static int November = 11;
        public static int December = 12;
    }

    public class TimeTrigger
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Schedule Schedule { get; set; }

        public int[] SelectedDaysOfWeek { get; set; }
        public int[] SelectedDaysOfMonth { get; set; }
        public int[] SelectedMonthsOfYear { get; set; }

        public TimeTrigger() 
            : this(DateTime.MinValue, DateTime.MaxValue, DaysOfWeek.All, DaysOfMonth.All, MonthsOfYear.All)
        {
        }
        public TimeTrigger(DateTime startDateTime, DateTime endDateTime, int[] daysOfWeek, int[] daysOfMonth, int[] monthsOfYear)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            SelectedDaysOfWeek = daysOfWeek;
            SelectedDaysOfMonth = daysOfMonth;
            SelectedMonthsOfYear = monthsOfYear;
        }
    }
}