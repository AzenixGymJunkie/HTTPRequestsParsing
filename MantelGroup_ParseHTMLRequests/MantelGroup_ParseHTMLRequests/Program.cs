using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MantelGroup_ParseHTMLRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory + @"\Reece_edited_data.log"); // providing location of log file we are parsing
            Dictionary<string, int> ip_frequency = new Dictionary<string, int>(); // Dictionary of all different ip addresses and the frequency/activeness of each
            Dictionary<string, int> urls = new Dictionary<string, int>(); // Dictionary of all URL's and frequency of each

            using (StreamReader sr = new StreamReader(path)) 
            {
                string line;
                string ip;
                string url; 

                while ((line = sr.ReadLine()) != null) // will keep reading until the end of the file
                {
                    // Console.WriteLine(line); // used for debugging/testing 

                    ip = line.Split(" ")[0]; // based on format of log file, ip address is always given first
                    url = line.Split("]")[1].Split("GET")[1].Split("HTTP")[0].Trim(); // extracting URL

                    // Console.WriteLine(url); // used for debugging/testing
                    // Console.WriteLine(ip); // used for debugging/testing

                    if (ip_frequency.ContainsKey(ip)) // already has the ip in the dictionary, meaning its count should increment
                    {
                        ip_frequency[ip]++;
                    }
                    else
                    {
                        ip_frequency.Add(ip, 1); // not in dictionary, so we will add it now
                    }

                    if (urls.ContainsKey(url))
                    {
                        urls[url]++;
                    }
                    else
                    {
                        urls.Add(url, 1); // not in dictionary, so we will add it now
                    }
                }
            }

            /*foreach(KeyValuePair<string,int> ip in ip_frequency)
            {
                Console.WriteLine(ip.Key + " appears " + ip.Value + " times in the log file."); // used for debugging/testing
            }*/

            List<KeyValuePair<string, int>> ip_frequency_sorted = ip_frequency.ToList(); // to be used for determining top 3
            ip_frequency_sorted.Sort((x, y) => x.Value.CompareTo(y.Value)); // sorts each entry based on its frequency
            ip_frequency_sorted.Reverse(); // want in descending order

            Console.WriteLine("The top three active IP addresses are:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(ip_frequency_sorted[i].Key + " (occuring " + ip_frequency_sorted[i].Value + " times)");
            }

            Console.WriteLine(); // used to keep things clean, extra line

            Console.WriteLine("The number of unique IP addresses are " + ip_frequency_sorted.Count);

            Console.WriteLine(); // used to keep things clean, extra line

            List<KeyValuePair<string, int>> url_sorted = urls.ToList(); // to be used for determining top 3
            url_sorted.Sort((x, y) => x.Value.CompareTo(y.Value)); // sorts each entry based on its frequency
            url_sorted.Reverse(); // want in descending order

            Console.WriteLine("The top three active URLs are:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(url_sorted[i].Key + " (occuring " + url_sorted[i].Value + " times)");
            }
        }
    }
}