using System;
using System.Collections.Generic;
using System.IO;
using Common.Logging;
using Commons.Utils;
using Commons.UI.LayoutDataStore.Properties;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// Allowed saved layout data for form stores
    /// <rus>
    /// позвол€ет сохранить настройки формы с ее контролами</rus>
    /// </summary>
    public class LayoutStoreWorker : ILayoutStoreWorker
    {
    	#region logging

    	protected static readonly ILog LOG =
    		LogManager.GetLogger(typeof (LayoutStoreWorker));

    	#endregion

        private const string ConfigFileExt = "xml";
        private const string DefaultPostfix = "def";
        protected INamableControl form;
        private string configFileName;
        private ILayoutDataStorePathFactory layoutDataStorePathFactory;

        protected internal IList<ILayoutDataStore> stores;

        public LayoutStoreWorker(ILayoutDataStorePathFactory layoutDataStorePathFactory)
        {
            this.layoutDataStorePathFactory = layoutDataStorePathFactory;
        }

        /// <summary>
        /// used to directly set file name for layout data file
        /// </summary>
        /// <param name="layoutDataStorePathFactory"></param>
        /// <param name="onlyFileName"></param>
        /// <param name="stores"></param>
        public LayoutStoreWorker(ILayoutDataStorePathFactory layoutDataStorePathFactory,
                                 string onlyFileName, IList<ILayoutDataStore> stores)
            : this(layoutDataStorePathFactory)
        {
            if (string.IsNullOrEmpty(onlyFileName))
                throw new ArgumentNullException("onlyFileName", Resource.FileNameCanNotBeEmpty);
            configFileName = layoutDataStorePathFactory.GetConfigFileName(onlyFileName, ConfigFileExt);
            this.stores = stores;
        }

        public LayoutStoreWorker(ILayoutDataStorePathFactory layoutDataStorePathFactory, string onlyFileName,
                                 params ILayoutDataStore[] stores)
            : this(layoutDataStorePathFactory, onlyFileName, stores as IList<ILayoutDataStore>)
        {
        }



        /// <summary>
        /// constructor 
        /// <rus>
        /// конструктор</rus>
        /// </summary>
        /// <param name="layoutDataStorePathFactory"></param>
        /// <param name="form"> winform on which needs to be store stores layout data 
        /// <rus>форма, на которой нужно сохранить расположение контролек</rus>
        /// </param>
        /// <param name="stores"> control list for layout data store
        ///                    <rus>список контролов чье расположение нужно сохранить </rus>
        /// </param>
        public LayoutStoreWorker(ILayoutDataStorePathFactory layoutDataStorePathFactory,
                                 INamableControl form, IList<ILayoutDataStore> stores)
            : this(layoutDataStorePathFactory)
        {
            if ((form == null) || (string.IsNullOrEmpty(form.Name)))
                throw new ArgumentNullException("form", Resource.NameOfTheFormMustBeUnic);
            this.form = form;
            this.stores = stores;
        }


        public LayoutStoreWorker(ILayoutDataStorePathFactory layoutDataStorePathFactory, INamableControl form,
                                 params ILayoutDataStore[] stores)
            : this(layoutDataStorePathFactory, form, (IList<ILayoutDataStore>) stores)
        {
        }

        public ILayoutDataStorePathFactory LayoutDataStorePathFactory
        {
            get { return layoutDataStorePathFactory; }
            set { layoutDataStorePathFactory = value; }
        }

        #region ILayoutStoreWorker Members

        /// <summary>
        /// filename of Config-file
        /// <rus>
        /// им€ конфигурационного файла</rus>
        /// </summary>
        public string ConfigFileName
        {
            get
            {
                ///если им€ не пусто, то выставл€етс€ вручную
                if (string.IsNullOrEmpty(configFileName))
                    configFileName = LayoutDataStorePathFactory.GetConfigFileName(form.Name, ConfigFileExt);
                return configFileName;
            }
            set { configFileName = value; }
        }

        /// <summary>
        /// saved layout data for form stores
        /// <rus>
        /// —охранить настройки формы и ее контролов</rus>
        /// </summary>
        public void Save()
        {
            LayoutSettings.WriteXml(ConfigFileName, stores);
        }

        /// <summary>
        /// saved layout data for form stores
        /// <rus>
        /// —охранить настройки формы и ее контролов</rus>
        /// </summary>
        public void SaveDefault()
        {
            LayoutSettings.WriteXml(DefaultLayout, stores);
        }

        /// <summary>
        /// filename of Config-file with Default values
        /// <rus>
        /// им€ конфигурационного файла c настройками по умолчанию</rus>
        /// </summary>
        public string DefaultLayout
        {
            get
            {
                return string.Format("{0}{1}.{2}"
                                     , Path.ChangeExtension(ConfigFileName, "")
                                     , DefaultPostfix
                                     , ConfigFileExt);
            }
        }


        /// <summary>
        /// load layout data for form stores
        /// <rus>
        /// «агрузить настройки формы и ее контролов</rus>
        /// </summary>
        public void Load()
        {
            FileInfo fileInfo = new FileInfo(FileUtils.MakeValidFilePath(ConfigFileName));
            if (!fileInfo.Exists)
            {
                LoadDefault();
                return;
            }
            try
            {
                LayoutSettings.ReadXml(ConfigFileName, false, stores);
            }
            catch (Exception e)
            {
				LOG.Error(e);
				throw new ApplicationException(Resource.LoadingIsInterruptedWillBeLoadedByDefault,e);
            }
        }

        /// <summary>
        /// set user interface control allowing to load a state of Entity by default.
        /// <rus>
        /// установить контрольку, котора€ даст пользователю возможность 
        /// загрузить состо€ние Entity, прин€тое по умолчанию</rus>
        /// </summary>
        public void SetUI4LoadDefault()
        {
            if (stores != null)
                foreach (ILayoutDataStore store in stores)
                {
                    store.SetUI4LoadDefault(this);
                }
        }

        /// <summary>
        /// load default layout data for form stores
        /// <rus>
        /// «агрузить настройки формы и ее контролов по умолчанию</rus>
        /// </summary>
        public void LoadDefault()
        {
            FileInfo fileInfo = new FileInfo(FileUtils.MakeValidFilePath(DefaultLayout));
            if (!fileInfo.Exists)
                return;
            LayoutSettings.ReadXml(DefaultLayout, true, stores);
        }

        /// <summary>
        /// load default layout data for control
        /// <rus>
        /// «агрузить настройку по умолчанию дл€ конкретного контрола</rus>
        /// </summary>
        public void LoadDefault(ILayoutDataStore control)
        {
            LoadStoreFromFile(DefaultLayout, control);
        }

        /// <summary>
        /// load layout data for control
        /// <rus>
        /// «агрузить настройку дл€ конкретного контрола</rus>
        /// </summary>
        public void Load(ILayoutDataStore control)
        {
            LoadStoreFromFile(ConfigFileName, control);
        }

        #endregion

        private static void LoadStoreFromFile(string fileName, ILayoutDataStore store)
        {
            FileInfo fileInfo = new FileInfo(FileUtils.MakeValidFilePath(fileName));
            if (!fileInfo.Exists)
                return;
            LayoutSettings.ReadXml(fileName, store, true);
        }

		public void Init()
		{
			SaveDefault();
			SetUI4LoadDefault();
			Load();
		}
    }
}
