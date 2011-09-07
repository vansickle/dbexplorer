using System;
using System.IO;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// common class for LayoutDataStore classes
    /// </summary>
    public abstract class AbstractLayoutDataStore : ILayoutDataStore
    {
        protected string checkCode;
        protected object entity;
        protected string name;
        protected ILayoutStoreWorker worker;

        /// <summary>
        /// constructor where we defined entity and flag of generating entity check code
        /// </summary>
        /// <param name="entity">Entity for which will be saving layout</param>
        /// <param name="name">Unique complex name of control</param>
        /// <param name="generateCheckCode">flag default or not generate entity check code</param>
        protected AbstractLayoutDataStore(object entity, string name, bool generateCheckCode)
        {
            this.entity = entity;
            this.name = name;
            if (generateCheckCode)
            {
                checkCode = GenerateCheckCode(Save());
            }
        }


        protected AbstractLayoutDataStore(object entity, string name) : this(entity, name, true)
        {
        }

        #region ILayoutDataStore Members

        /// <summary>
        /// Check code of entity
        /// <rus>
        /// контрольный код сущности</rus>
        /// </summary>
        public string CheckCode
        {
            get { return (checkCode); }
        }

        /// <summary>
        /// saving layout info of entity
        /// <rus>сохраняет данные о расположении объекта</rus>
        /// </summary>
        /// <returns>
        /// saved data
        /// <rus>
        /// сохраненные данные</rus></returns>
        public string Save()
        {
            MemoryStream memory = new MemoryStream();
            SaveEntity2Memory(memory);
            return LayoutStoreUtils.Stream2String(memory);
        }

        /// <summary>
        /// loading layout info of entity
        /// <rus>загружает данные о расположении объекта</rus>
        /// </summary>
        /// <param name="xmlLayoutData">
        /// data with layout info 
        /// <rus>
        /// данные о расположении объекта</rus></param>
        public abstract void Load(string xmlLayoutData);

        /// <summary>
        /// Entity for which will be saving layout
        /// </summary>
        public object Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// set control for User Interface loading layout data by default
        /// </summary>
        /// <param name="lsworker">object with method LoadDefaultLayout or semi method</param>
        public virtual void SetUI4LoadDefault(ILayoutStoreWorker lsworker)
        {
            worker = lsworker;
        }

        /// <summary>
        /// Entity name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

    	/// <summary>
    	/// saving for defined entity
    	/// </summary>
    	/// <param name="memory">layout data</param>
    	protected abstract void SaveEntity2Memory(Stream memory);

        /// <summary>
        /// generate entity check code
        /// </summary>
        /// <param name="dataForCheckCode">data for generate</param>
        /// <returns></returns>
        public static string GenerateCheckCode(string dataForCheckCode)
        {
            return LayoutStoreUtils.GetMD5Encrypted(dataForCheckCode);
        }

        protected void LoadDefaultLayout(object sender, EventArgs e)
        {
            worker.LoadDefault(this);
        }
    }
}
