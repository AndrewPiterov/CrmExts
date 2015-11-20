using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace CrmExts.Extensions.Test
{
    public enum PluginMessage
    {
        Undefined = 0,
        Create, Update, Delete
    }

    public abstract class TestablePlugin : IPlugin
    {
        public IOrganizationService Service { get; private set; }
        public Entity TargetEntity { get; private set; }

      //  public string Message { get; private set; }
        public PluginMessage PluginMessage { get; private set; }

        public bool IsActionStarted { get; set; }

        /// <summary>
        /// TragetEntity в том виде в котором сейчас храниться в ЦРМ
        /// </summary>
        public Entity PersistEntity
        {
            get
            {
                var e = Service.Retrieve(TargetEntity.LogicalName, TargetEntity.Id, new ColumnSet(true));
                return e;
            }
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                DefinePluginMessage(context.MessageName);

                // exit from loop
                if (PluginMessage == PluginMessage.Update && context.Depth > 1)
                    return;

                //if (!context.InputParameters.Contains("Target") ||
                //    !(context.InputParameters["Target"] is Entity))
                //    return;

                // In case of SetState and SetStateDynamicEntity message InputParameter
                // would contain EntityMoniker parameter 
                if (context.InputParameters.Contains("EntityMoniker") &&
                    context.InputParameters["EntityMoniker"] is EntityReference)
                {
                    var reference = (EntityReference)context.InputParameters["EntityMoniker"];
                    TargetEntity = new Entity
                    {
                        LogicalName = reference.LogicalName,
                        Id = reference.Id
                    };
                }

                if (context.InputParameters.Contains("Target") && (context.InputParameters["Target"] is Entity))
                    TargetEntity = (Entity)context.InputParameters["Target"];

                if (context.InputParameters.Contains("Target") && (context.InputParameters["Target"] is EntityReference))
                {
                    var reference = (EntityReference)context.InputParameters["Target"];

                    TargetEntity = new Entity
                    {
                        LogicalName = reference.LogicalName,
                        Id = reference.Id
                    };
                }

                Service = ((IOrganizationServiceFactory)serviceProvider
                    .GetService(typeof(IOrganizationServiceFactory)))
                    .CreateOrganizationService(context.UserId);

                if (TargetEntity == null)
                    throw new Exception("TargetEntity not defened.");

                if (IsValidToExecute)
                {
                    Action();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(string.Format("Ошибка плагина {0}: {1}", GetType().Name, ex.Message));
            }
        }

        private void DefinePluginMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            switch (message.ToLower())
            {
                case "create":
                    PluginMessage = PluginMessage.Create;
                    break;
                case "update":
                    PluginMessage = PluginMessage.Update;
                    break;
                case "delete":
                    PluginMessage = PluginMessage.Delete;
                    break;
                default:
                    PluginMessage = PluginMessage.Undefined;
                    break;
            }
        }

        /// <summary>
        /// Условия для запуска плагина
        /// </summary>
        /// <returns></returns>
        protected abstract bool IsValidToExecute { get; }

        protected abstract void Action();

        public void Execute(Entity targetEntity, IOrganizationService service, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }

            if (targetEntity == null)
            {
                throw new ArgumentNullException("targetEntity");
            }

            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            TargetEntity = targetEntity;
            Service = service;

            DefinePluginMessage(message);

            if (IsValidToExecute)
            {
                Action();
            }
        }
    }
}
