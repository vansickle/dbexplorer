using System.Collections.Generic;
using System.Xml;
using Commons.Utils;

namespace Commons.UI.LayoutDataStore
{
    public class LayoutSettings
    {
        private const string CheckElement = "CheckCode";
        private const string ControlElement = "Control";
        private const string DataElement = "Data";
        private const string LayoutControlsElement = "Control";
        private const string NameElement = "Name";

        /// <summary>
        /// write layout data in xml-file for list of object
        /// </summary>
        /// <param name="fileName">name of xml-file</param>
        /// <param name="stores">list of object for writing layout data</param>
        public static void WriteXml(string fileName, IList<ILayoutDataStore> stores)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement element;
            IList<ILayoutDataStore> controls = stores;
            // create a comment/pi and append
            if (controls == null) return;
            XmlNode node = doc.CreateComment(string.Format("Saved layout information for {0} controls of form", controls.Count));
            doc.AppendChild(node);
            node = doc.CreateElement(LayoutControlsElement);
            doc.AppendChild(node);

            foreach (ILayoutDataStore control in controls)
            {
                element = CreateElement(doc, ControlElement, NameElement, control.Name);
                node.AppendChild(element);
                element.AppendChild(CreateElement(doc, CheckElement, control.CheckCode));
                element.AppendChild(CreateCDataSection(doc, DataElement, control.Save()));
            }

            XmlTextWriter tw = new XmlTextWriter(FileUtils.MakeValidFilePath(fileName), null);
            tw.Formatting = Formatting.Indented;
            doc.Save(tw);
            tw.Close();
        }

        private static XmlElement CreateElement(XmlDocument doc, string name, string attrname, string attrvalue)
        {
            XmlElement element = doc.CreateElement(name);
            XmlAttribute attr = doc.CreateAttribute(attrname);
            attr.Value = attrvalue;
            element.SetAttributeNode(attr);
            return element;
        }

        private static XmlElement CreateElement(XmlDocument doc, string name, string value)
        {
            XmlElement element = doc.CreateElement(name);
            element.InnerText = value;
            return element;
        }


        private static XmlElement CreateCDataSection(XmlDocument doc, string name, string value)
        {
            XmlElement element = doc.CreateElement(name);
            element.AppendChild(doc.CreateCDataSection(value));
            return element;
        }

        /// <summary>
        /// read object layout data from xml-file
        /// </summary>
        /// <param name="fileName">name of xml-file</param>
        /// <param name="store">object for reading his layout data</param>
        /// <param name="noCheckCode">control check code of object</param>
        public static void ReadXml(string fileName, ILayoutDataStore store, bool noCheckCode)
        {
            LoadLayoutDataStore(GetElemList(FileUtils.MakeValidFilePath(fileName)), store, noCheckCode);
        }

        /// <summary>
        /// read layout data from xml-file for list of object
        /// </summary>
        /// <param name="fileName">name of xml-file</param>
        /// <param name="noCheckCode">control check code of object</param>
        /// <param name="stores">list of object for reading layout data</param>
        public static void ReadXml(string fileName, bool noCheckCode, IList<ILayoutDataStore> stores)
        {
            XmlNodeList elemList = GetElemList(FileUtils.MakeValidFilePath(fileName));
            foreach (ILayoutDataStore store in stores)
            {
                LoadLayoutDataStore(elemList, store, noCheckCode);
            }
        }

        private static XmlNodeList GetElemList(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FileUtils.MakeValidFilePath(fileName));
            return doc.GetElementsByTagName(ControlElement);
        }

        private static void LoadLayoutDataStore(XmlNodeList elemList, ILayoutDataStore store, bool noCheckCode)
        {
            foreach (XmlElement e in elemList)
            {
                if (e.GetAttribute(NameElement) == store.Name)
                {
                    if (e.GetElementsByTagName(CheckElement)[0].InnerText == store.CheckCode || noCheckCode)
                    {
                        XmlNode childNode = e.GetElementsByTagName(DataElement)[0].ChildNodes[0];
                        if (childNode is XmlCDataSection)
                        {
                            XmlCDataSection cdataSection = childNode as XmlCDataSection;
                            store.Load(cdataSection.Value);
                        }
                        else
                            store.Load(e.GetElementsByTagName(DataElement)[0].InnerText);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// previously we use arrays of stores - now collections - so this method provides backword support and
        /// some sugar to use
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stores"></param>
        public static void WriteXml(string fileName, params ILayoutDataStore[] stores)
        {
            WriteXml(FileUtils.MakeValidFilePath(fileName), (IList<ILayoutDataStore>) stores);
        }
    }
}
