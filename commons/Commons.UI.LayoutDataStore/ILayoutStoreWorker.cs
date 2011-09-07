namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// Allowed saved layout data for form controls
    /// <rus>
    /// ��������� ��������� ��������� ����� � �� ����������</rus>
    /// </summary>
    public interface ILayoutStoreWorker
    {
        /// <summary>
        /// filename of Config-file
        /// <rus>
        /// ��� ����������������� �����</rus>
        /// </summary>
        string ConfigFileName { get; set; }

        /// <summary>
        /// saved layout data for form controls
        /// <rus>
        /// ��������� ��������� ����� � �� ���������</rus>
        /// </summary>
        void Save();

        /// <summary>
        /// saved layout data for form controls
        /// <rus>
        /// ��������� ��������� ����� � �� ���������</rus>
        /// </summary>
        void SaveDefault();

        /// <summary>
        /// filename of Config-file with Default values
        /// <rus>
        /// ��� ����������������� ����� c ����������� �� ���������</rus>
        /// </summary>
        string DefaultLayout { get; }

        /// <summary>
        /// load layout data for form controls
        /// <rus>
        /// ��������� ��������� ����� � �� ���������</rus>
        /// </summary>
        void Load();

        /// <summary>
        /// load default layout data for form controls
        /// <rus>
        /// ��������� ��������� ����� � �� ��������� �� ���������</rus>
        /// </summary>
        void LoadDefault();

        /// <summary>
        /// load default layout data for control
        /// <rus>
        /// ��������� ��������� �� ��������� ��� ����������� ��������</rus>
        /// </summary>
        void LoadDefault(ILayoutDataStore control);

        /// <summary>
        /// load default layout data for control
        /// <rus>
        /// ��������� ��������� ��� ����������� ��������</rus>
        /// </summary>
        void Load(ILayoutDataStore control);

        /// <summary>
        /// set user interface control allowing to load a state of Entity by default.
        /// <rus>
        /// ���������� ����������, ������� ���� ������������ ����������� 
        /// ��������� ��������� Entity, �������� �� ���������</rus>
        /// </summary>
        void SetUI4LoadDefault();

    	void Init();
    }
}
