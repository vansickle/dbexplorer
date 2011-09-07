using System;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// To store layout data of object
    /// <rus>���������� ������ ������������ ������� </rus>
    /// </summary>
    public interface ILayoutDataStore
    {
        /// <summary>
        /// object which layout data we're saving
        /// <rus>������, ������ � ������������ �������� ����� ���������</rus> 
        /// </summary>
        Object Entity { get; }

        /// <summary>
        /// check version code for entity
        /// <rus>
        /// ����������� ���, �� �������� ����� �������� �� ������ �������</rus>
        /// </summary>
        string CheckCode { get; }

        /// <summary>
        /// used to originally identify name of control
        /// set is used for nested controls
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// saved layout data
        /// <rus>
        /// ��������� ������ � ������������ �������</rus>
        /// </summary>
        /// <returns> layout data of Entity in XML-format
        ///      <rus>������ � ������������ ������� � XML-�������</rus>
        /// </returns>
        string Save();

        /// <summary>
        /// load layout data
        /// <rus>��������� ������ � ������������ �������</rus>
        /// </summary>
        /// <param name=
        ///    "xmlLayoutData"> - layout data of Entity in XML-format
        ///                  <rus>������ � ������������ ������� � XML-�������</rus>
        /// </param>
        void Load(string xmlLayoutData);

        /// <summary>
        /// set user interface control allowing to load a state of Entity by default.
        /// <rus>
        /// ���������� ����������, ������� ���� ������������ ����������� 
        /// ��������� ��������� Entity, �������� �� ���������</rus>
        /// </summary>
        /// <param name="worker"> reference of object allowing saved layout data on a form
        ///                 <rus> ������ �� ������, ����������� ������������ ����������, ����������� � �����</rus>
        /// </param>
        void SetUI4LoadDefault(ILayoutStoreWorker worker);
    }
}
