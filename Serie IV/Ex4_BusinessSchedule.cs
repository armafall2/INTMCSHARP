using System;
using System.Collections.Generic;
using System.Linq;

namespace Serie_IV
{
    public class BusinessSchedule
    {
        private DateTime defaultStartDate = new DateTime(2020, 1, 1);
        private DateTime defaultEndDate = new DateTime(2030, 12, 31);
        private SortedDictionary<DateTime, List<TimeSpan>> schedule = new SortedDictionary<DateTime, List<TimeSpan>>();

        public bool IsEmpty()
        {
            bool isEmpty = false;
            
                if(schedule.Count == 0)
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
            return isEmpty;
        }

        public void SetRangeOfDates(DateTime begin, DateTime end)
        {
            try
            {
                if (end <= begin || begin < defaultStartDate || end > defaultEndDate)
                {
                    throw new ArgumentException("Période invalide");
                }

                if (!IsEmpty())
                {
                    throw new InvalidOperationException("L'emploi du temps doit être vide avant de définir la période.");
                }

                defaultStartDate = begin;
                defaultEndDate = end;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la définition de la période : {ex.Message}");
            }
        }

        public KeyValuePair<DateTime, DateTime> ClosestElements(DateTime beginMeeting)
        {
            KeyValuePair<DateTime, DateTime> closestElements = new KeyValuePair<DateTime, DateTime>();

            try
            {
                DateTime lowerBound = schedule.ContainsKey(beginMeeting) ? beginMeeting : schedule.Keys.LastOrDefault(x => x <= beginMeeting);
                DateTime upperBound = schedule.Keys.FirstOrDefault(x => x > beginMeeting);

                closestElements = new KeyValuePair<DateTime, DateTime>(lowerBound, upperBound);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la recherche des éléments les plus proches : {ex.Message}");
            }

            return closestElements;
        }

        public bool AddBusinessMeeting(DateTime date, TimeSpan duration)
        {
            try
            {
                var closestElements = ClosestElements(date);

                if ((closestElements.Key == null || !schedule.ContainsKey(closestElements.Key) || date >= closestElements.Key.Add(schedule[closestElements.Key].LastOrDefault())) &&
                    (closestElements.Value == null || !schedule.ContainsKey(closestElements.Value) || date.Add(duration) <= closestElements.Value))
                {
                    if (!schedule.ContainsKey(date))
                    {
                        schedule[date] = new List<TimeSpan>();
                    }

                    schedule[date].Add(duration);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'ajout de la réunion : {ex.Message}");
                return false;
            }
        }

        public bool DeleteBusinessMeeting(DateTime date, TimeSpan duration)
        {
            try
            {
                if (schedule.ContainsKey(date) && schedule[date].Contains(duration))
                {
                    schedule[date].Remove(duration);

                    if (schedule[date].Count == 0)
                    {
                        schedule.Remove(date);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression de la réunion : {ex.Message}");
                return false;
            }
        }

        public int ClearMeetingPeriod(DateTime begin, DateTime end)
        {
            try
            {
                if (begin < defaultStartDate || end > defaultEndDate || end <= begin)
                {
                    throw new ArgumentException("Période invalide");
                }

                int meetingsRemoved = 0;
                foreach (var date in schedule.Keys.ToArray())
                {
                    if (date >= begin && date <= end)
                    {
                        meetingsRemoved += schedule[date].Count;
                        schedule.Remove(date);
                    }
                }

                return meetingsRemoved;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du nettoyage de la période de réunions : {ex.Message}");
                return 0;
            }
        }

        public void DisplayMeetings()
        {
            try
            {
                if (IsEmpty())
                {
                    Console.WriteLine("Pas de réunions programmées");
                    return;
                }

                Console.WriteLine($"Période étudiée : {defaultStartDate.ToString("dd/MM/yyyy HH:mm:ss")} - {defaultEndDate.ToString("dd/MM/yyyy HH:mm:ss")}");

                foreach (var entry in schedule)
                {
                    DateTime date = entry.Key;
                    List<TimeSpan> meetings = entry.Value;
                    
                    Console.WriteLine($"Date : {date.ToString("dd/MM/yyyy HH:mm:ss")}");

                    foreach (var duration in meetings)
                    {
                        Console.WriteLine($"Réunion : {duration}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'affichage des réunions : {ex.Message}");
            }
        }
    }
}
