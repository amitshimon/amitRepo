using System;
using System.Collections.Generic;
using System.Linq;



namespace DataAccess
{
    public class SourceData
    {
        private DataAccessContext _context;
        private Logger _logger;
        public Logger Logger
        {
            get
            {
                try
                {
                    if (_logger == null)
                    {
                        _logger = new Logger();
                        return _logger;
                    }
                }
                catch (Exception ex)
                {

                }
                return _logger;
            }
        }
        public DataAccessContext Context
        {
            get
            {
                try
                {
                    if (_context == null)
                    {
                        _context = new DataAccessContext();
                        return _context;
                    }
                    return _context;
                }
                catch (Exception ex)
                {
                    Logger.Fatal("Context in SourceData" + ex.InnerException);
                }
                return _context;
            }
        }


        //Function Convert From Ticks(long) to 
        //DateTime 
        //parameter long ticks
        private DateTime ConvertTickToDate(long ticks)
        {
            try
            {
                DateTime webDate = new DateTime(ticks);
                return webDate;
            }
            catch (Exception ex)
            {
                Logger.Fatal("ConvertTickToDate Function in SourceData" + ex.InnerException);
            }
            return DateTime.Now;
        }

        //Function accepts EntityModel
        //And convert it to WebEntityModel obj
        //parameter EntityModel entity
        private WebEntityModel ConvertToWeb(EntityModel entity)
        {
            try
            {
                WebEntityModel webEntityModel = new WebEntityModel
                {
                    id = entity.id,
                    description = entity.description,
                    amount = entity.amount,
                    isPrivate = entity.isPrivate,
                    name = entity.name,
                    date = ConvertTickToDate(entity.date)
                };
                return webEntityModel;
            }
            catch (Exception ex)
            {
                Logger.Fatal("ConvertToWeb Function in SourceData"+ex.InnerException);
            }
            return null;
        }

        //Returns list of all entitys
        public List<WebEntityModel> GetAll()
        {
            try
            {
                List<WebEntityModel> webList = new List<WebEntityModel>();
                foreach (var entity in Context.entityContext.ToList())
                {
                    webList.Add(ConvertToWeb(entity));
                }
                return webList;
            }
            catch (Exception ex)
            {
                Logger.Fatal("GetAll Function in SourceData");
                return null;
            }
        }
        //Returns web object Entity by id
        //Parameter Guid id
        public WebEntityModel GetEntityById(Guid id)
        {
            try
            {
                return ConvertToWeb(Context.entityContext.FirstOrDefault(entity => entity.id == id));
            }
            catch (Exception ex)
            {
                Logger.Fatal("GetEntityById function in SourceData" + ex.InnerException);
                return null;
            }
        }
        //Returns db object Entity by id
        //Parameter Guid id
        public EntityModel GetDbEntityById(Guid id)
        {
            try
            {
                return Context.entityContext.FirstOrDefault(entity => entity.id == id);
            }
            catch (Exception ex)
            {
                Logger.Fatal("GetEntityById function in SourceData" + ex.InnerException);
                return null;
            }
        }
        //Function updates entity from user
        //Get two parameters entity object 
        //And Guid id for the entity      
        public bool UpdateEntity(Guid id, EntityModel entity)
        {
            EntityModel entityModel;
            try
            {
                entityModel = GetDbEntityById(id);
                if (entityModel != null)
                {
                    try
                    {
                        entityModel.id = entity.id;
                        entityModel.name = entity.name;
                        entityModel.description = entity.description;
                        entityModel.date = entity.date;
                        entityModel.amount = entity.amount;
                        entityModel.isPrivate = entity.isPrivate;
                        Context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Fatal("entityModel function in SourceData" + ex.InnerException);
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        //Create Entity Function
        //Parameters  EntityModel from user
        //returns boolean 
        public bool CreateEntity(EntityModel entity)
        {
            try
            {
                Context.entityContext.Add(entity);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal("CreateEntity function in SourceData" + ex.InnerException);
                return false;
            }
        }
        public bool DeleteEntity(Guid id)
        {
            try
            {
                Context.entityContext.Remove(GetDbEntityById(id));
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal("DeleteEntity function in SourceData" + ex.InnerException);
                return false;
            }
        }
    }
}
