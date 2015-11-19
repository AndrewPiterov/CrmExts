using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions.Models
{
    public abstract class Model
    {
        //  public readonly Guid ModelID;

        public readonly Entity Entity;

        public Guid EntityId
        {
            get
            {
                return Entity.Id;
            }
        }


        //protected Model()
        //{
        //    //  ModelID = Guid.NewGuid();

        //    if (string.IsNullOrEmpty(LogicalName))
        //    {
        //        throw new Exception("Logical Name Not Defened");

        //    }

        //    Entity = new Entity
        //    {
        //        LogicalName = LogicalName,
        //        Id = Guid.NewGuid()
        //    };
        //}

        protected Model()
        {
            if (LogicalName != null)
                Entity = new Entity(LogicalName) { Id = Guid.NewGuid() };
        }

        protected Model(Entity entity)
        {
            // ModelID = Guid.NewGuid();

            if (entity == null) throw new ArgumentNullException("entity");

            //  EntityId = entity.Id;

            Entity = entity;

            // MapEntityToModel(entity);
        }

        //  protected abstract void MapEntityToModel(Entity entity);

        public abstract string LogicalName { get; }
    }
}
