using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
    class State
    {
        public int[] Table;
        public int Priority;
        
        //Default State
        public State(int[] array)
        {
            
        }
        public bool Hash() //对状态的判重
        {
            throw new NotImplementedException();
        }
        public void Next() //接下来的状态压入队列
        {
            throw new NotImplementedException();
        }
        public interface IComparer
        {

        }
    }
    class Search
    {
        public SortedList<IComparer<State>, int> slist;
        public List<State> list;
        //SortedList<State>plist = new 
    }
}
