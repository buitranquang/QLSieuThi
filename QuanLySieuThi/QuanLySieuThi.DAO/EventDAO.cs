using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.DAO
{
    public class EventDAO
    {
        private QuanLySieuThiContext context = new QuanLySieuThiContext();

        public EventDAO()
        {
        }

        public int Save(Event eventModel, List<EventDetail> eventDetails)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Events.Add(eventModel);
                    context.SaveChanges();
                    context.Entry(eventModel).State = EntityState.Detached;
                    foreach (var detail in eventDetails)
                    {
                        detail.EventID = eventModel.ID;
                        detail.ProductID = detail.Product.ID;
                        detail.Product = null;
                        context.EventDetails.Add(detail);
                    }

                    int res = context.SaveChanges();
                    transaction.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public int Update(Event eventModel)
        {
            try
            {
                var existingEvent = context.Events.Find(eventModel.ID);

                if (existingEvent == null)
                {
                    throw new Exception($"Event with ID {eventModel.ID} not found.");
                }

                existingEvent.StartDate = eventModel.StartDate;
                existingEvent.EndDate = eventModel.EndDate;
                existingEvent.Description = eventModel.Description;

                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public Event GetById(int id)
        {
            return context.Events.Find(id);
        }

        public List<Event> GetCurrentEvents()
        {
            DateTime currentDate = DateTime.Now.Date;
            return context.Events
                     .Where(e => e.StartDate <= currentDate && (!e.EndDate.HasValue || e.EndDate >= currentDate))
                     .ToList();
        }

        public Event GetCurrentEvent()
        {
            DateTime currentDate = DateTime.Now.Date;
            return context.Events
                     .FirstOrDefault(e => e.StartDate <= currentDate && (!e.EndDate.HasValue || e.EndDate >= currentDate));
        }

        public List<Event> GetEventsFromNow()
        {
            DateTime currentDate = DateTime.Now.Date;
            return context.Events
                     .Where(e => e.EndDate >= currentDate)
                     .ToList();
        }

        public int RemoveDetail(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    EventDetail ed = context.EventDetails.Find(id);
                    Event evt = ed.Event;
                    context.EventDetails.Remove(ed);
                    int res = context.SaveChanges();
                    if (evt.EventDetails.Count == 0)
                    {
                        context.Events.Remove(evt);
                    }
                    res += context.SaveChanges();
                    transaction.Commit();
                    return res;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public int Delete(int evtID)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Event evt = context.Events.Find(evtID);
                    int res = 0;
                    List<EventDetail> edList = (List<EventDetail>) context.EventDetails.Where(ed => ed.EventID == evt.ID).ToList();
                    foreach (EventDetail ed in edList)
                    {
                        context.EventDetails.Remove(ed);
                    }
                    
                    context.Events.Remove(evt);
                    res += context.SaveChanges();
                    transaction.Commit();
                    return res;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }

}
