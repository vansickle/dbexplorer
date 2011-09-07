using System;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// To store layout data of object
    /// <rus>сохранение данных расположени€ объекта </rus>
    /// </summary>
    public interface ILayoutDataStore
    {
        /// <summary>
        /// object which layout data we're saving
        /// <rus>объект, данные о расположении которого хотим сохранить</rus> 
        /// </summary>
        Object Entity { get; }

        /// <summary>
        /// check version code for entity
        /// <rus>
        /// контрольный код, по которому видно мен€лась ли верси€ объекта</rus>
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
        /// сохранить данные о расположении объекта</rus>
        /// </summary>
        /// <returns> layout data of Entity in XML-format
        ///      <rus>данные о расположении объекта в XML-формате</rus>
        /// </returns>
        string Save();

        /// <summary>
        /// load layout data
        /// <rus>загрузить данные о расположении объекта</rus>
        /// </summary>
        /// <param name=
        ///    "xmlLayoutData"> - layout data of Entity in XML-format
        ///                  <rus>данные о расположении объекта в XML-формате</rus>
        /// </param>
        void Load(string xmlLayoutData);

        /// <summary>
        /// set user interface control allowing to load a state of Entity by default.
        /// <rus>
        /// установить контрольку, котора€ даст пользователю возможность 
        /// загрузить состо€ние Entity, прин€тое по умолчанию</rus>
        /// </summary>
        /// <param name="worker"> reference of object allowing saved layout data on a form
        ///                 <rus> ссылка на объект, сохран€ющий расположение контрольки, прив€занной к форме</rus>
        /// </param>
        void SetUI4LoadDefault(ILayoutStoreWorker worker);
    }
}
