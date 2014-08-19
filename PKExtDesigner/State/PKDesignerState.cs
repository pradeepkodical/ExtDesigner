using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;
using PKExtFramework.Persistance;
using PKExtDesigner.Designer;

namespace PKExtDesigner.State
{
    internal class PKDesignerState
    {
        private Stack<string> undoBuffers = new Stack<string>();
        private Stack<string> redoBuffers = new Stack<string>();
        private string lastState;
        private ExtDesignerUI currentPage;
        private bool working;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public PKDesignerState(ExtDesignerUI page)
        {            
            currentPage = page;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Checkpoint()
        {
            if (!working)
            {
                string str = PKStorage.Serialize(currentPage.AppPage);
                if (str != lastState)
                {
                    undoBuffers.Push(str);
                    lastState = str;
                }
            }
        }
        /// <summary>
        /// Ctrl - Z
        /// </summary>
        public void UnDo()
        {
            working = true;
            if (undoBuffers.Count > 0)
            {
                string str = undoBuffers.Pop();                
                if (str != null)
                {
                    currentPage.SetAppPage(PKStorage.Deserialize(str) as PKControl);
                    redoBuffers.Push(str);
                }
            }
            working = false;
        }

        /// <summary>
        /// Ctrl - Y
        /// </summary>
        public void ReDo()
        {
            working = true;
            if (redoBuffers.Count > 0)
            {
                string str = redoBuffers.Pop();                
                if (str != null)
                {
                    currentPage.SetAppPage(PKStorage.Deserialize(str) as PKControl);
                    undoBuffers.Push(str);
                }
            }
            working = false;
        }
    }
}
