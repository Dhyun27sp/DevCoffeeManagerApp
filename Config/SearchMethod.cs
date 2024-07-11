using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DevCoffeeManagerApp.Config
{
    public class SearchMethod
    {
        private static string ConvertString(string stringInput)// hàm chuyển đổi chữ tiếng việt thành không đấu viết thường
        {
            stringInput = stringInput.ToLower();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To      = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                stringInput = stringInput.Replace(convert[i], To[i]);
            }
            return stringInput;
        }
        private static bool check_sign(string name)// hàm kiểm tra cụm từ vừa dấu vừa không
        {

            int index_word_have_sign = 0;
            int index_word_not_have_sign = 0;
            string[] words = name.Split(' ');

            // In từng từ ra màn hình
            foreach (string word in words)
            {
                bool word_have_sign = word.Normalize(NormalizationForm.FormD).Any(c => CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark);
                if (word_have_sign)
                {
                    index_word_have_sign++;
                }
                else
                {
                    index_word_not_have_sign++;
                }
            }
            if (index_word_have_sign != 0 && index_word_not_have_sign != 0)
            {
                return true;
            }
            return false;
        }

        public static bool Search_have_key(string name, string key)//hàm kiểm tra đầu vào key
        {
            bool word_have_sign = key.Normalize(NormalizationForm.FormD).Any(c => CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark);

            bool contains = false;
            string name_convertUnicode;
            string search_convertUnicode;
            if (word_have_sign == true)
            {
                if (check_sign(key) == true)
                {
                    name_convertUnicode = ConvertString(name).Replace(" ", "");
                    search_convertUnicode = ConvertString(key).Replace(" ", "");
                    bool contains_not_sign = name_convertUnicode.Contains(search_convertUnicode);
                    contains = contains_not_sign;
                }
                else
                {
                    bool contains_sign = name.Replace(" ", "").IndexOf(key.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0;
                    contains = contains_sign;
                    if (contains_sign == false && key.Count() > 1)
                    {
                        name_convertUnicode = ConvertString(name).Replace(" ", "");
                        search_convertUnicode = ConvertString(key).Replace(" ", "");
                        bool contains_not_sign = name_convertUnicode.Contains(search_convertUnicode);
                        contains = contains_not_sign;
                    }
                }
            }
            else
            {
                name_convertUnicode = ConvertString(name).Replace(" ", "");
                search_convertUnicode = ConvertString(key).Replace(" ", "");
                bool contains_not_sign = name_convertUnicode.Contains(search_convertUnicode);
                contains = contains_not_sign;
            }
            return contains;
        }
    }
}
