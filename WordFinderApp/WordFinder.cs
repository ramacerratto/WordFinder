using BenchmarkDotNet.Attributes;
using WordFinderApp.Interfaces;

namespace WordFinderApp
{
    
    public class WordFinder : IWordFinder
    {
        private readonly List<string> _lines;
        private Dictionary<string, int> _wordFrequency;

        /// <summary>
        /// Constructor for the WordFinder service. 
        /// This initialize the matrix in lines to search on them
        /// </summary>
        /// <param name="matrix">List of Strings that represents a matrix</param>
        public WordFinder(IEnumerable<string> matrix)
        {
            _wordFrequency = new Dictionary<string, int>();

            List<string> rows = matrix.ToList();
            int numCols = 0;
            if (rows.Count != 0)
            {
                numCols = rows[0].Length;
            }

            // Build columns
            List<string> cols = new();
            for (int col = 0; col < numCols; col++)
            {
                char[] colArray = new char[rows.Count];
                for (int row = 0; row < rows.Count; row++)
                {
                    colArray[row] = rows[row][col];
                }
                cols.Add(new string(colArray));
            }
            _lines = new List<string>(rows);
            _lines.AddRange(cols); //I add every line to a single List
        }

        /// <summary>
        /// This method searchs a word stream inside the initialized matrix
        /// </summary>
        /// <param name="wordstream">A stream of words to find in the matrix</param>
        /// <returns>List of top 10 words found</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream, string method = "contain")
        {
            HashSet<string> uniqueWords = new HashSet<string>();
            
            foreach (var word in wordstream)
            {
                uniqueWords.Add(word); // HashSet to check each word once
            }
            
            foreach (var word in uniqueWords)
            {
                if(method == "contain")
                {
                    FindByContain(word);
                }
                else
                {
                    FindBySuffixArray(word);
                }
            }

            return _wordFrequency
                .OrderByDescending(w => w.Value)
                .ThenBy(w => w.Key)
                .Take(10)
                .Select(w => w.Key);
        }

        /// <summary>
        /// Find using the Contain method
        /// </summary>
        /// <param name="word">word to search</param>
        private void FindByContain(string word)
        {
            //Just look in each line of the matrix
            foreach (var line in _lines)
            {
                if (line.Contains(word))
                {
                    WordFound(word);
                }
            }
        }

        /// <summary>
        /// Find using the Suffix Array method
        /// </summary>
        /// <param name="word">word to search</param>
        private void FindBySuffixArray(string word)
        {
            var suffixArrayMatrix = new Dictionary<int,int[]>();
            int i = 0;
            //For each line of the matrix
            foreach (var line in _lines)
            {
                //Add the suffix only if not created yet
                if (!suffixArrayMatrix.ContainsKey(i))
                {
                    suffixArrayMatrix.Add(i,BuildLineSuffixArray(line));
                }
                if (BinarySearchInSuffixArray(word, line, suffixArrayMatrix[i]))
                {
                    WordFound(word);
                }
                i++;
            }
        }

        /// <summary>
        /// Method to encapsulate the instructions when a word is found
        /// </summary>
        /// <param name="word">word to search</param>
        private void WordFound(string word)
        {
            if (!_wordFrequency.ContainsKey(word))
            {
                _wordFrequency[word] = 0; //Initialize the counter
            }
            _wordFrequency[word]++; //Adds found word
        }

        /// <summary>
        /// This method creates the SuffixArray to search the word
        /// </summary>
        /// <param name="text">string of matrix</param>
        /// <returns>array of indices of the sorted suffixes/returns>
        private int[] BuildLineSuffixArray(string text)
        {
            int n = text.Length;
            var suffixes = new (int Index, string Suffix)[n];

            // Step 1: Generate all suffixes
            for (int i = 0; i < n; i++)
            {
                suffixes[i] = (i, text.Substring(i));
            }

            // Step 2: Sort the suffixes based on the lexicographical order
            suffixes = suffixes.OrderBy(suf => suf.Suffix).ToArray();

            // Step 3: Store the indices of the sorted suffixes
            int[] suffixArray = new int[n];
            for (int i = 0; i < n; i++)
            {
                suffixArray[i] = suffixes[i].Index;
            }

            return suffixArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word">word to search</param>
        /// <param name="text">string of matrix</param>
        /// <param name="suffixArray">suffix array used to search</param>
        /// <returns></returns>
        private bool BinarySearchInSuffixArray(string word, string text, int[] suffixArray)
        {
            int left = 0, right = suffixArray.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                string suffix = text.Substring(suffixArray[mid]);

                // Compare the prefix of the suffix with the word
                int compare = string.Compare(suffix, 0, word, 0, word.Length);

                if (compare == 0)
                {
                    return true; // Word is found
                }
                if (compare < 0)
                {
                    left = mid + 1; // Search in the right half
                }
                else
                {
                    right = mid - 1; // Search in the left half
                }
            }

            return false; // Word not found
        }
    }
}
