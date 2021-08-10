﻿//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : SecondlyFrequency.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/22/2014
// Note    : Copyright 2003-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class used to implements the Secondly frequency rules
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/20/2004  EFW  Created the code
//===============================================================================================================

using System;

#nullable disable
namespace EWSoftware.PDI
{
    /// <summary>
    /// This implements the Secondly frequency rules
    /// </summary>
    internal sealed class SecondlyFrequency : IFrequencyRules
    {
        /// <summary>
        /// This is used to find the starting point for the secondly frequency
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="start">The recurrence start date</param>
        /// <param name="end">The recurrence end date</param>
        /// <param name="from">The start date of the range limiting the instances generated</param>
        /// <param name="to">The end date of the range limiting the instances generated</param>
        /// <returns>The first instance date or null if there are no more instances</returns>
        public RecurDateTime FindStart(Recurrence r, RecurDateTime start, RecurDateTime end, RecurDateTime from,
          RecurDateTime to)
        {
            RecurDateTime rdt = new RecurDateTime(start);
            int adjust;

            if(RecurDateTime.Compare(start, from, RecurDateTime.DateTimePart.Hour) < 0)
            {
                // Get the difference between the recurrence start and the limiting range start
                DateTime dtStart = start.ToDateTime(), dtFrom = from.ToDateTime();

                // Adjust the date/time so that it's in range
                TimeSpan ts = dtFrom - dtStart;

                adjust = (int)ts.TotalSeconds + r.Interval - 1;
                rdt.AddSeconds(adjust - (adjust % r.Interval));
            }

            if(RecurDateTime.Compare(rdt, end, RecurDateTime.DateTimePart.Hour) > 0 ||
              RecurDateTime.Compare(rdt, to, RecurDateTime.DateTimePart.Hour) > 0)
                return null;

            return rdt;
        }

        /// <summary>
        /// This is used to find the next instance of the secondly frequency
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="end">The recurrence end date</param>
        /// <param name="to">The end date of the range limiting the instances generated</param>
        /// <param name="last">This is used to pass in the last instance date calculated and return the next
        /// instance date</param>
        /// <returns>True if the recurrence has another instance or false if there are no more instances</returns>
        public bool FindNext(Recurrence r, RecurDateTime end, RecurDateTime to, RecurDateTime last)
        {
            last.AddSeconds(r.Interval);

            if(last > end || last > to)
                return false;

            return true;
        }

        /// <summary>
        /// This is used to filter the secondly frequency by month
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByMonth(Recurrence r, RecurDateTimeCollection dates)
        {
            return Filter.ByMonth(r, dates);
        }

        /// <summary>
        /// ByWeekNo is only applicable in the Yearly frequency and is ignored for the Secondly frequency
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByWeekNo(Recurrence r, RecurDateTimeCollection dates)
        {
            return dates.Count;
        }

        /// <summary>
        /// This is used to filter the secondly frequency by year day
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByYearDay(Recurrence r, RecurDateTimeCollection dates)
        {
            return Filter.ByYearDay(r, dates);
        }

        /// <summary>
        /// This is used to filter the secondly frequency by month day
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByMonthDay(Recurrence r, RecurDateTimeCollection dates)
        {
            return Filter.ByMonthDay(r, dates);
        }

        /// <summary>
        /// This is used to filter the secondly frequency by day of the week
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByDay(Recurrence r, RecurDateTimeCollection dates)
        {
            return Filter.ByDay(r, dates);
        }

        /// <summary>
        /// This is used to filter the secondly frequency by hour
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByHour(Recurrence r, RecurDateTimeCollection dates)
        {
            return Filter.ByHour(r, dates);
        }

        /// <summary>
        /// This is used to filter the secondly frequency by minute
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int ByMinute(Recurrence r, RecurDateTimeCollection dates)
        {
            return Filter.ByMinute(r, dates);
        }

        /// <summary>
        /// This is used to filter the secondly frequency by second
        /// </summary>
        /// <param name="r">A reference to the recurrence</param>
        /// <param name="dates">A reference to the collection of current instances that have been generated</param>
        /// <returns>The number of instances in the collection.  If zero, subsequent rules don't have to be
        /// checked as there's nothing else to do.</returns>
        public int BySecond(Recurrence r, RecurDateTimeCollection dates)
        {
            int count = dates.Count;

            // Don't bother if either collection is empty
            if(count != 0 && r.BySecond.Count != 0)
                for(int idx = 0, nCollIdx = 0; idx < count; idx++)
                {
                    // Remove the date/time if the second isn't wanted
                    if(!r.isSecondUsed[dates[nCollIdx].Second])
                    {
                        dates.RemoveAt(nCollIdx);
                        count--;
                        idx--;
                    }
                    else
                        nCollIdx++;
                }

            return dates.Count;
        }
    }
}
