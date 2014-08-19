using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PKExtFramework.Ext.Component;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Persistance;
using System.IO;
using PKExtDesigner.Visitors;
using PKExtDesigner.State;
using PKExtDesigner.CodeGen;
using System.Diagnostics;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Utility;
using PKExtFramework.Ext.Component.Panel;
using PKExtFramework.Ext.Component.Grid;
using PKExtFramework.Ext.Component.Tab;
using PKExtDesigner.Designer;

namespace PKExtDesigner
{
    public partial class ExtApplication : Form
    {
        #region Member
        
        private event Action DesignChange;
        private bool propertyGridFocus = false;
        private PKDesignerState stateManager;
        private ExtDesignerUI pkExtDesigner;
        
        #endregion

        public ExtApplication()
        {
            InitializeComponent();
            InitPKDesigner();

            this.mnuAddUserControl.Tag = new PKControl { IsComponent = true};
            this.mnuAddContainer.Tag = new PKContainer();            
            this.mnuAddPanel.Tag = new PKPanel();
            this.mnuAddGrid.Tag = new PKGrid();
            this.mnuAddTabPanel.Tag = new PKTabPanel();
            this.mnuAddTabPage.Tag = new PKTab();

            this.mnuAddColumn.Tag = new PKColumn();

            this.mnuAddText.Tag = new PKText();
            this.mnuAddButton.Tag = new PKButton();

            this.mnuAddField.Tag = new PKField();
            this.mnuAddCheckField.Tag = new PKField();
            this.mnuAddRadioField.Tag = new PKField();
            this.mnuAddDateField.Tag = new PKField();
            this.mnuAddComboField.Tag = new PKField();
            this.mnuAddNumberField.Tag = new PKField();
            this.mnuAddTextArea.Tag = new PKField();
            
            InitEventHandlers();

            UpdateTree();
        }

        #region Initialization

        private void InitPKDesigner()
        {
            this.pkExtDesigner = new ExtDesignerUI
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = "pkExtDesigner",
                Size = new System.Drawing.Size(713, 503),
                TabIndex = 3
            };

            this.splitContainer1.Panel1.Controls.Add(this.pkExtDesigner);

            this.pkExtDesigner.SetAppPage(new PKControl
            {
                Application = "PKExtApp",
                Layout = new PKVBox()
            });

            this.stateManager = new PKDesignerState(pkExtDesigner);            
        }

        private void InitEventHandlers()
        {
            this.pkExtDesigner.SelectedChange += new Action<PKBoxItem>(pkExtDesigner1_SelectedChange);
            this.pkExtDesigner.ItemAdded += new Action<PKBoxItem>(pkExtDesigner1_ItemAdded);
            this.pkExtDesigner.ItemDeleted += new Action(pkExtDesigner1_ItemDeleted);
            this.pkExtDesigner.ItemMoved += new Action(pkExtDesigner1_ItemMoved);
            
            this.DesignChange += new Action(PkExtApplication_DesignChange);

            this.pkExtDesigner.MouseClick += new MouseEventHandler(this.pkExtDesigner1_MouseClick);

            this.treeView1.DragDrop += new DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DragEnter += new DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.ItemDrag += new ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.DragOver += new DragEventHandler(this.treeView1_DragOver);
            
            this.propertyGrid1.Leave += new EventHandler(this.propertyGrid1_Leave);
            this.propertyGrid1.Enter += new EventHandler(this.propertyGrid1_Enter);
            this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.btnUndo.Click += new EventHandler(this.btnUndo_Click);
            this.btnRedo.Click += new EventHandler(this.btnRedo_Click);
            this.btnExt.Click += new EventHandler(this.btnExt_Click);

            this.btnMakeSameSize.Click += new EventHandler(btnMakeSameSize_Click);

            this.mnuAddContainer.Click += new EventHandler(this.mnuAddContainer_Click);
            this.mnuAddPanel.Click += new EventHandler(this.mnuAddPanel_Click);
            this.mnuAddField.Click += new EventHandler(this.mnuAddField_Click);
            this.mnuAddNumberField.Click += new EventHandler(this.mnuAddNumberField_Click);
            this.mnuAddDateField.Click += new EventHandler(this.mnuAddDateField_Click);
            this.mnuAddComboField.Click += new EventHandler(this.mnuAddComboField_Click);
            this.mnuAddCheckField.Click += new EventHandler(this.mnuAddCheckField_Click);
            this.mnuAddRadioField.Click += new EventHandler(this.mnuAddRadioField_Click);
            this.mnuAddButton.Click += new EventHandler(this.mnuAddButton_Click);
            this.mnuAddTextArea.Click += new EventHandler(this.mnuAddTextArea_Click);
            this.mnuAddText.Click += new EventHandler(this.mnuAddText_Click);
            this.mnuAddGrid.Click += new EventHandler(this.mnuAddTable_Click);
            this.mnuAddColumn.Click += new EventHandler(this.mnuAddColumn_Click);
            this.mnuAddTabPanel.Click += new EventHandler(mnuAddTabPanel_Click);
            this.mnuAddTabPage.Click += new EventHandler(mnuAddTabPage_Click);
            this.mnuAddUserControl.Click += new EventHandler(mnuAddUserControl_Click);

            this.mnuCut.Click += new EventHandler(this.mnuCut_Click);
            this.mnuCopy.Click += new EventHandler(this.mnuCopy_Click);
            this.mnuPaste.Click += new EventHandler(this.mnuPaste_Click);
            this.mnuDelete.Click += new EventHandler(this.mnuDelete_Click);            
        }

        private void pkExtDesigner1_ItemMoved()
        {
            stateManager.Checkpoint();
            UpdateTree();
        }

        private void PkExtApplication_DesignChange()
        {
            stateManager.Checkpoint();
        }

        #endregion

        #region Form Key Event Handlers
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
                CopySelected();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.X))
            {
                CutSelected();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.V))
            {
                PasteSelected();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Z))
            {
                stateManager.UnDo();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Y))
            {
                stateManager.ReDo();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Up) || keyData == (Keys.Control | Keys.Left))
            {
                pkExtDesigner.MoveUp();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Down) || keyData == (Keys.Control | Keys.Right))
            {
                pkExtDesigner.MoveDown();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Shift | Keys.Left))
            {
                pkExtDesigner.DecreaseWidth();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Shift | Keys.Right))
            {
                pkExtDesigner.IncreaseWidth();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Shift | Keys.Down))
            {
                pkExtDesigner.IncreaseHeight();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Shift | Keys.Up))
            {
                pkExtDesigner.DecreaseHeight();
                return true;
            }
            else if (keyData == Keys.Delete && !propertyGridFocus)
            {
                pkExtDesigner.DeleteSelected();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Private Methods
        private void UpdateTree()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            pkExtDesigner.AppPage.Accept(new PKTreeNodeVisitor(treeView1.Nodes));            
            treeView1.EndUpdate();
        }

        private bool SelectNode(TreeNodeCollection treeNodeCollection, PKBoxItem obj)
        {
            foreach (TreeNode node in treeNodeCollection)
            {
                if (node.Tag == obj)
                {
                    node.Text = string.Format("{0} ({1})", obj.Name, obj.TypeName);
                    treeView1.SelectedNode = node;
                    return true;
                }
                if (SelectNode(node.Nodes, obj)) {
                    return true;
                }
            }
            return false;
        }

        private void CutSelected()
        {
            CopySelected();
            pkExtDesigner.DeleteSelected();
        }

        private void CopySelected()
        {
            Clipboard.Clear();            
            if (pkExtDesigner.SelectedItem != null)
            {
                string tempObject = PKStorage.Serialize(pkExtDesigner.SelectedItem);
                Clipboard.SetText(tempObject);
            }
        }

        private void PasteSelected()
        {
            try
            {
                string tempObject = Clipboard.GetText();
                if (tempObject != null)
                {
                    PKBoxItem obj = PKStorage.Deserialize(tempObject, true);
                    pkExtDesigner.AddItem(obj);
                }
            }
            catch
            { 
            }
        }
        #endregion
        
        #region PKExtDesigner Event Handlers
        private void pkExtDesigner1_ItemDeleted()
        {
            UpdateTree();
            treeView1.ExpandAll();
            pkExtDesigner.SelectItem(null, false);
            this.DesignChange();
        }

        private void pkExtDesigner1_ItemAdded(PKBoxItem obj)
        {
            UpdateTree();
            pkExtDesigner.SelectItem(obj, false);
            this.DesignChange();
        }

        private void pkExtDesigner1_SelectedChange(PKBoxItem obj)
        {
            SelectNode(treeView1.Nodes, obj);
            propertyGrid1.SelectedObject = obj;
        }

        private void pkExtDesigner1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && pkExtDesigner.SelectedItem != null)
            {
                pkExtDesigner.SelectedItem.Accept(
                    new PKContextMenuVisitor(this.contextMenuStrip1, pkExtDesigner, e.X, e.Y));
            }
        }

        #endregion

        #region Property Grid Event Handlers
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            pkExtDesigner.DoLayout();
            pkExtDesigner.SelectItem(pkExtDesigner.SelectedItem, false);
            this.DesignChange();
        }
        private void propertyGrid1_Enter(object sender, EventArgs e)
        {
            propertyGridFocus = true;
        }

        private void propertyGrid1_Leave(object sender, EventArgs e)
        {
            propertyGridFocus = false;
        }
        #endregion

        #region ToolBar Handlers

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PKControl cmp = this.pkExtDesigner.AppPage;
                //cmp.ComponentFileName = Path.GetFileName(saveFileDialog1.FileName);
                //cmp.IsComponent = true;
                string strJSON = PKStorage.Serialize(cmp);
                
                File.WriteAllText(saveFileDialog1.FileName, strJSON);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strJSON = File.ReadAllText(openFileDialog1.FileName);
                PKBoxItem item = PKStorage.Deserialize(strJSON);
                pkExtDesigner.SetAppPage(item as PKControl);
            }
        }
        
        private void btnUndo_Click(object sender, EventArgs e)
        {
            stateManager.UnDo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            stateManager.ReDo();
        }

        private void btnExt_Click(object sender, EventArgs e)
        {
            PKExtCodeGenerator.Generate(pkExtDesigner.AppPage);
        }

        private void btnMakeSameSize_Click(object sender, EventArgs e)
        {
            pkExtDesigner.MakeSameSizeFields();
        }

        #endregion

        #region TreeView Handlers
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pkExtDesigner.SelectItem(treeView1.SelectedNode.Tag as PKBoxItem, false);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pkExtDesigner.SelectItem(e.Node.Tag as PKBoxItem, false);
                (e.Node.Tag as PKBoxItem).Accept(
                    new PKContextMenuVisitor(this.contextMenuStrip1, treeView1, e.X, e.Y));
            }
        }
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode destinationNode = ((TreeView)sender).GetNodeAt(pt);
                TreeNode newNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                TreeNode sourceNode = newNode.Parent;
                if (destinationNode != sourceNode && destinationNode != newNode)
                {
                    newNode.Remove();
                    var item = newNode.Tag as PKBoxItem;
                    item.Remove();

                    var visitor = new PKItemAddVisitor(item);
                    (destinationNode.Tag as PKBoxItem).Accept(visitor);
                    if (visitor.Added)
                    {
                        destinationNode.Nodes.Add(newNode);
                    }
                    else
                    {
                        (sourceNode.Tag as PKBoxItem).Accept(visitor);
                        sourceNode.Nodes.Add(newNode);
                    }

                    treeView1.SelectedNode = newNode;
                    pkExtDesigner.DoLayout();
                }
            }
        }

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode destinationNode = ((TreeView)sender).GetNodeAt(pt);
                TreeNode moveNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;

                if (moveNode != null && destinationNode != null)
                {
                    var visitor = new PKItemCanAddVisitor(moveNode.Tag as PKBoxItem);
                    (destinationNode.Tag as PKBoxItem).Accept(visitor);
                    if (visitor.CanAdd)
                    {
                        treeView1.SelectedNode = destinationNode;
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
        }
        #endregion

        #region Context Menu Handlers

        private void mnuAddContainer_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKContainer
            {
                Name = "pnl" + PKSequenceGenerator.GetNextGUIId(),
                Width = 200,
                Height = 200,
                Flex = 1,
                Padding = 5,
                Layout = new PKVBox()
            });
        }

        private void mnuAddUserControl_Click(object sender, EventArgs e)
        {
            var form = new UserComponentsUI();
            form.ControlSelected += new Action<PKControl>(form_ControlSelected);
            form.Show();
        }

        private void form_ControlSelected(PKControl selectedControl)
        {
            this.pkExtDesigner.AddItem(selectedControl);
        }

        private void mnuAddButton_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKButton
            {
                Name = "btn" + PKSequenceGenerator.GetNextGUIId(),
                Value = "Button",                
                Width = 70
            });
        }

        private void mnuAddField_Click(object sender, EventArgs e)
        {
            var field = new PKField {
                Name = "txt" + PKSequenceGenerator.GetNextGUIId(),                
                Width = 200                
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align  = PKHAlign.Right,                
                Width = 120                
            });
            field.Add(new PKTextField
            {
                Value = "Text",
                Width = 120                
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddComboField_Click(object sender, EventArgs e)
        {
            var field = new PKField
            {
                Name = "cmb" + PKSequenceGenerator.GetNextGUIId(),                
                Width = 200
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align = PKHAlign.Right,                
                Width = 120
            });
            field.Add(new PKComboField
            {
                Value = "Text",
                Width = 120                
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddDateField_Click(object sender, EventArgs e)
        {
            var field = new PKField
            {
                Name = "dt" + PKSequenceGenerator.GetNextGUIId(),                
                Width = 200
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align = PKHAlign.Right,                
                Width = 120
            });
            field.Add(new PKDateField
            {
                Value = DateTime.Today.ToString("MM/dd/yyyy"),
                Width = 120                
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddCheckField_Click(object sender, EventArgs e)
        {
            var field = new PKField
            {
                Name = "chk" + PKSequenceGenerator.GetNextGUIId(),                
                Width = 200
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align = PKHAlign.Right,                
                Width = 120
            });
            field.Add(new PKCheckField
            {
                Width = 120                
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddRadioField_Click(object sender, EventArgs e)
        {
            var field = new PKField
            {
                Name = "rad" + PKSequenceGenerator.GetNextGUIId(),                
                Width = 200
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align = PKHAlign.Right,                
                Width = 120
            });
            field.Add(new PKRadioField
            {
                Width = 120                
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddNumberField_Click(object sender, EventArgs e)
        {
            var field = new PKField
            {
                Name = "num" + PKSequenceGenerator.GetNextGUIId(),                
                Width = 200
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align = PKHAlign.Right,                
                Width = 120
            });
            field.Add(new PKNumberField
            {
                Width = 120
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddTextArea_Click(object sender, EventArgs e)
        {
            var field = new PKField
            {
                Name = "txtArea" + PKSequenceGenerator.GetNextGUIId(),
                Height = 140,
                Width = 300
            };
            field.Add(new PKLabelField
            {
                Value = "Label:",
                Align = PKHAlign.Right,                
                Width = 120
            });
            field.Add(new PKTextAreaField
            {
                Width = 220
            });
            pkExtDesigner.AddItem(field);
        }

        private void mnuAddTabPanel_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKTabPanel
            {
                Name = "tabPnl" + PKSequenceGenerator.GetNextGUIId(),
                Width = 200,
                Height = 200,
                Flex = 1                
            });
        }

        private void mnuAddTabPage_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKTab
            {
                Name = "pnl" + PKSequenceGenerator.GetNextGUIId(),
                Text = "Tab Page",
                Width = 200,
                Height = 200,
                Flex = 1
            });
        }   

        private void mnuAddPanel_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKPanel
            {
                Name = "pnl" + PKSequenceGenerator.GetNextGUIId(),
                Width = 200,
                Height = 200,
                Flex = 1
            });
        }      

        private void mnuAddText_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKText
            {
                Name = "txt" + PKSequenceGenerator.GetNextGUIId(),
                Value = "Text",
                Height = 30,
                Width = 100,
                Flex = 1
            });
        }        

        private void mnuAddTable_Click(object sender, EventArgs e)
        {
            var table = new PKGrid
            {
                Name = "grid" + PKSequenceGenerator.GetNextGUIId(),
                Height = 30,
                Width = 100,
                Flex = 1                
            };
            table.ColumnRow.BorderPen = PKPens.BorderPen;            
            pkExtDesigner.AddItem(table);
        }

        private void mnuAddColumn_Click(object sender, EventArgs e)
        {
            pkExtDesigner.AddItem(new PKColumn
            {
                Name = "col" + PKSequenceGenerator.GetNextGUIId(),
                Value = "Column",
                Height = 30,
                Width = 100,
                Flex = 1                
            });
        }
        
        private void mnuCut_Click(object sender, EventArgs e)
        {
            CutSelected();
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            CopySelected();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            PasteSelected();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            pkExtDesigner.DeleteSelected();
        }
        #endregion        
    }
}
