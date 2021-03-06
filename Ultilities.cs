using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDVXStarter
{
    /// <summary>
    /// Provide handful functions to stupid Neskol some how did not bother 
    /// to find if any exsiting functions can be used.
    /// </summary>
    public static class Ultilities
    {
        /// <summary>
        /// Incase Neskol forgot what's already in set.
        /// </summary>
        /// <param name="set">Set to find</param>
        /// <param name="name">Name to find</param>
        /// <returns>True if contained, false elsewise</returns>
        public static bool FindDuplicates(List<string> set, string name)
        {
            bool result = false;

            foreach (string x in set)
            {
                if (x.Equals(name))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Incase Neskol forgot what's already in set.
        /// </summary>
        /// <param name="set">Set to find</param>
        /// <param name="name">Name to find</param>
        /// <returns>True if contained, false elsewise</returns>
        public static bool FindDuplicates(ComboBox.ObjectCollection set, string name)
        {
            bool result = false;

            foreach (string x in set)
            {
                if (x.Equals(name))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Incase Neskol forgot what's already in set.
        /// </summary>
        /// <param name="set">Set to find</param>
        /// <param name="name">Name to find</param>
        /// <param name="isKey">True to find in keys, false to find in values</param>
        /// <returns>True if contained, false elsewise</returns>
        public static bool FindDuplicates(Dictionary<string,string> set, string name, bool isKey)
        {
            bool result = false;

            if (isKey)
            {
                foreach (string x in set.Keys)
                {
                    if (x.Equals(name))
                    {
                        result = true;
                    }
                }
            }
            else
            {
                foreach (string x in set.Values)
                {
                    if (x.Equals(name))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// For command lines to properly process.
        /// </summary>
        /// <param name="path">Whole path to take</param>
        /// <returns>Disk name of path</returns>
        public static string GetDiskName(string path)
        {
            string[] disk = path.Split('\\');
            return disk[0];
        }

        /// <summary>
        /// Checks if given path contains spice tools.
        /// </summary>
        /// <param name="path">Given path</param>
        /// <param name="strict">True for all must contain, false for either spice&cfg exist or 64&cfg exist</param>
        /// <returns>True if contains, false elsewise</returns>
        public static bool ContainSpice(string path, bool strict)
        {
            bool spiceExist = File.Exists(path + "spice.exe");
            bool spice64Exist = File.Exists(path + "spice64.exe");
            bool spiceCfgExist = File.Exists(path + "spicecfg.exe");
            bool exist;
            if (strict)
            {
                exist = spiceExist && spice64Exist && spiceCfgExist;
            }
            else
            {
                exist = (spiceExist && spiceCfgExist) || (spice64Exist && spiceCfgExist);
            }
            return exist;
        }

        /// <summary>
        /// Find given path of subfolder which father path contains spice tools
        /// </summary>
        /// <param name="path">Path of subfolder</param>
        /// <param name="strict">True requires all spice, spice64 and spicesfg contains,
        /// false requires either of spice and spice64 and spicecfg exists</param>
        /// <returns>Father path that contains spice tools</returns>
        public static string FindFatherPathContainSpice(string path, bool strict)
        {
            string result = path;
            string[] pathGroup = path.Split('\\');
            bool exists = ContainSpice(path, strict);

            if(!exists)
            {
                string currentPath = "";
                for (int i = 0; i<pathGroup.Length&&!exists;i++)
                {
                    currentPath += (pathGroup[i]+"\\");
                    exists = ContainSpice(currentPath, strict);
                }
                result = currentPath;
            }
            return result;
        }

        /// <summary>
        /// Adds item prevent having duplicates.
        /// </summary>
        /// <param name="receiver">List to add</param>
        /// <param name="add">Item to add</param>
        public static void SafeAdd(List<string> receiver, string add)
        {
            if (!FindDuplicates(receiver,add))
            {
                receiver.Add(add);
            }
        }

        /// <summary>
        /// Adds item prevent having duplicates.
        /// </summary>
        /// <param name="receiver">List to add</param>
        /// <param name="add">Item to add</param>
        public static void SafeAdd(ComboBox.ObjectCollection receiver, string add)
        {
            if (!FindDuplicates(receiver, add))
            {
                receiver.Add(add);
            }
        }

        /// <summary>
        /// Return a set without command items that cause troubles.
        /// </summary>
        /// <param name="set">Set to take in</param>
        /// <returns>New set without command items</returns>
        public static List<string> TrimCommands(List<string> set)
        {
            List<string> result = new List<string>();
            foreach (string item in set)
            {
                if (!item.Equals("(Remove)")&& !item.Equals("(Add)") && !item.Equals("(Empty)") && !item.Equals("Default") && !item.Equals("Offline"))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// Return a set without command items that cause troubles.
        /// </summary>
        /// <param name="set">Set to take in</param>
        /// <returns>New set without command items</returns>
        public static List<string> TrimCommands(ComboBox.ObjectCollection set)
        {
            List<string> result = new List<string>();
            foreach (string item in set)
            {
                if (!item.Equals("(Remove)") && !item.Equals("(Add)") && !item.Equals("(Empty)") && !item.Equals("Default") && !item.Equals("Offline"))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
