namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// Allowed saved layout data for form controls
    /// <rus>
    /// позволяет сохранить настройки формы с ее контролами</rus>
    /// </summary>
    public interface ILayoutStoreWorker
    {
        /// <summary>
        /// filename of Config-file
        /// <rus>
        /// имя конфигурационного файла</rus>
        /// </summary>
        string ConfigFileName { get; set; }

        /// <summary>
        /// saved layout data for form controls
        /// <rus>
        /// Сохранить настройки формы и ее контролов</rus>
        /// </summary>
        void Save();

        /// <summary>
        /// saved layout data for form controls
        /// <rus>
        /// Сохранить настройки формы и ее контролов</rus>
        /// </summary>
        void SaveDefault();

        /// <summary>
        /// filename of Config-file with Default values
        /// <rus>
        /// имя конфигурационного файла c настройками по умолчанию</rus>
        /// </summary>
        string DefaultLayout { get; }

        /// <summary>
        /// load layout data for form controls
        /// <rus>
        /// Загрузить настройки формы и ее контролов</rus>
        /// </summary>
        void Load();

        /// <summary>
        /// load default layout data for form controls
        /// <rus>
        /// Загрузить настройки формы и ее контролов по умолчанию</rus>
        /// </summary>
        void LoadDefault();

        /// <summary>
        /// load default layout data for control
        /// <rus>
        /// Загрузить настройку по умолчанию для конкретного контрола</rus>
        /// </summary>
        void LoadDefault(ILayoutDataStore control);

        /// <summary>
        /// load default layout data for control
        /// <rus>
        /// Загрузить настройку для конкретного контрола</rus>
        /// </summary>
        void Load(ILayoutDataStore control);

        /// <summary>
        /// set user interface control allowing to load a state of Entity by default.
        /// <rus>
        /// установить контрольку, которая даст пользователю возможность 
        /// загрузить состояние Entity, принятое по умолчанию</rus>
        /// </summary>
        void SetUI4LoadDefault();

    	void Init();
    }
}
