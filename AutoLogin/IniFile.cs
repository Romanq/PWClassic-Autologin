using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoLogin
{
    /*public class IniFile
    {
        string Path; //Имя файла.

        public FileInfo info;

        [DllImport("kernel32")] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, int nSize, string lpFileName);

        // С помощью конструктора записываем пусть до файла и его имя.
        public IniFile(string IniPath)
        {
            info = new FileInfo(IniPath);
            Path = info.FullName.ToString();
        }

        public List<string> SectionNames()
        {
            List<string> Sections = new List<string>();
            byte[] buffer = new byte[1024];
            GetPrivateProfileSectionNames(buffer, buffer.Length, Path);
            string allSections = System.Text.Encoding.UTF8.GetString(buffer);
            string[] sectionNames = allSections.Split('\0');
            foreach (string sectionName in sectionNames)
            {

                if (sectionName != string.Empty)
                {
                    if (sectionName == "GameSettings")
                        continue;

                    Sections.Add(sectionName);
                }
                    
            }
            // Returns All names as Items in Combobox
            return Sections;
        }

        //Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
        public string ReadINI(string Section, string Key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        //Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        //Удаляем ключ из выбранной секции.
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Section, Key, null);
        }
        //Удаляем выбранную секцию
        public void DeleteSection(string Section = null)
        {
            Write(Section, null, null);
        }
        //Проверяем, есть ли такой ключ, в этой секции
        public bool KeyExists(string Key, string Section = null)
        {
            return ReadINI(Section, Key).Length > 0;
        }
    }*/
}
