using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace DevCoffeeManagerApp.Commands.CommandOrder
{
    public class FilterByTypeCommand : CommandBase
    {
        private OrderViewModel orderFoodViewModel;
        public FilterByTypeCommand(OrderViewModel orderFoodViewModel)
        {
            this.orderFoodViewModel = orderFoodViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            orderFoodViewModel.Dishs = filter(orderFoodViewModel.AllDishsVariable, orderFoodViewModel.types_dish_search, orderFoodViewModel.Type, orderFoodViewModel.Type_Special);
        }
        public List<DishModel> filter(List<DishModel> AllDishsVariable, string types_dish_search, string Type, string Type_Special)// hàm fiter cho search, loại món, loại món đặt biệt
        {
            List<DishModel> Dishs_Search = new List<DishModel>();
            if (types_dish_search != "")
            {
                foreach (var item in AllDishsVariable)
                {
                    bool contains = Dish_have_key(item, types_dish_search);
                    if (contains)// trường hợp key có trong món ăn
                    {
                        if (Type != "All Dishs")
                        {
                            if (item.category == Type)
                            {
                                AddToResultList(ref Dishs_Search, item, Type_Special);
                            }
                        } 
                        else
                        {
                            AddToResultList(ref Dishs_Search, item, Type_Special);
                        }
                    }

                }
                if (Dishs_Search.Count == 0)//trương hợp key ko có trong món nào
                {
                    return null;
                }
            }
            else // trương hợ search rỗng 
            {
                AllDishsVariable = orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type);
                return sort_special_Dish_first(AllDishsVariable, Dishs_Search, Type_Special, Type);
            }
            return Dishs_Search;
        }
        private List<DishModel> DeleteduplicatesDish(List<DishModel> ListOnlyDisCont, List<DishModel> ListonlyType)// xóa món trùng nhau
        {
            List<DishModel> result = new List<DishModel>();
            result = ListonlyType.Where(item => !ListOnlyDisCont.Contains(item)).ToList();
            return result;
        }
        private List<DishModel> sort_special_type_comesfirst(List<DishModel> special_type_comesfirst, string special_type)
        {
            List<DishModel> sortedList = new List<DishModel>(); //sắp xếp món đặt biệt lên trước 
            if (special_type == "Discounted")
            {
                sortedList = special_type_comesfirst
                .OrderByDescending(x => x.SaleDish)
                .ToList();
            }
            else if (special_type == "New Dish")
            {
                sortedList = special_type_comesfirst
                .OrderByDescending(x => x.newDish)
                .ToList();
            }
            else if (special_type == "Hot Dish")
            {
                sortedList = special_type_comesfirst
                .OrderByDescending(x => x.HotDish)
                .ToList();
            }
            
            return sortedList;
            
        }
        private string ConvertString(string stringInput)// hàm chuyển đổi chữ tiếng việt thành không đấu viết thường
        {
            stringInput = stringInput.ToLower();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                stringInput = stringInput.Replace(convert[i], To[i]);
            }
            return stringInput;
        }
        private bool wordsign_notsign(string name)// hàm kiểm tra cụm từ vừa dấu vừa không
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
            if(index_word_have_sign != 0 && index_word_not_have_sign!=0)
            {
                return true;
            }
            return false;
        }

        private bool Dish_have_key(DishModel item,string key)//hàm kiểm tra đầu vào key
        {
            bool word_have_sign = key.Normalize(NormalizationForm.FormD).Any(c => CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark);
            
            bool contains = false;
            if (word_have_sign == true)
            {
                if (wordsign_notsign(key) == true)
                {
                    string itemconvertUnicode = ConvertString(item.dish_name).Replace(" ", "");
                    string types_dish_searchconvertUnicode = ConvertString(key).Replace(" ", "");
                    bool contains_not_sign = itemconvertUnicode.Contains(types_dish_searchconvertUnicode);
                    contains = contains_not_sign;
                }
                else
                {
                    bool contains_sign = item.dish_name.Replace(" ", "").IndexOf(key.Replace(" ", ""), StringComparison.OrdinalIgnoreCase) >= 0;
                    contains = contains_sign;
                    if (contains_sign == false && key.Count() > 1)
                    {
                        string itemconvertUnicode = ConvertString(item.dish_name).Replace(" ", "");
                        string types_dish_searchconvertUnicode = ConvertString(key).Replace(" ", "");
                        bool contains_not_sign = itemconvertUnicode.Contains(types_dish_searchconvertUnicode);
                        contains = contains_not_sign;
                    }
                }
            }
            else
            {
                string itemconvertUnicode = ConvertString(item.dish_name).Replace(" ", "");
                string types_dish_searchconvertUnicode = ConvertString(key).Replace(" ", "");
                bool contains_not_sign = itemconvertUnicode.Contains(types_dish_searchconvertUnicode);
                contains = contains_not_sign;
            }
            return contains;
        }

        private void AddToResultList(ref List<DishModel> Dishs_Search, DishModel item, string Type_Special)
        {
            if (Type_Special == "Discounted" && item.SaleDish)
            {
                Dishs_Search.Add(item);
            }
            else if (Type_Special == "New Dish" && item.newDish)
            {
                Dishs_Search.Add(item);
            }
            else if (Type_Special == "Hot Dish" && item.HotDish)
            {
                Dishs_Search.Add(item);
            }
            else
            {
                Dishs_Search.Add(item);
            }
        }

        private List<DishModel> sort_special_Dish_first(List<DishModel> AllDishsVariable, List<DishModel> Dishs_Search,string Type_Special,string Type)
        {
            if (Type_Special == "Discounted")
            {
                List<DishModel> combine = new List<DishModel>();
                List<DishModel> notdishcount = new List<DishModel>();
                notdishcount = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                combine.AddRange(Dishs_Search);
                combine.AddRange(notdishcount);
                return sort_special_type_comesfirst(combine, Type_Special);
            }
            else if (Type_Special == "New Dish")
            {
                List<DishModel> combine = new List<DishModel>();
                List<DishModel> notdishnew = new List<DishModel>();
                notdishnew = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                combine.AddRange(Dishs_Search);
                combine.AddRange(notdishnew);
                return sort_special_type_comesfirst(combine, Type_Special);
            }
            else if (Type_Special == "Hot Dish")
            {
                List<DishModel> combine = new List<DishModel>();
                List<DishModel> notdishHot = new List<DishModel>();
                notdishHot = DeleteduplicatesDish(Dishs_Search, orderFoodViewModel.LoadDishWithType(AllDishsVariable, Type));
                combine.AddRange(Dishs_Search);
                combine.AddRange(notdishHot);
                return sort_special_type_comesfirst(combine, Type_Special);
            }
            return AllDishsVariable;
        }
    }
}
