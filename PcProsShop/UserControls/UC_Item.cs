﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PcProsShop.UserControls
{
    public partial class UC_Item : UserControl
    {
        Form1 parentForm;
        UC_Home lastForm;
        Item item;

        private int itemAmount = 1;
        private int inventoryAmount;

        public UC_Item(Form1 form, UC_Home previousForm, Item loadedItem)
        {
            InitializeComponent();
            this.parentForm = form;
            this.item = loadedItem;
            this.lastForm = previousForm;
            LoadItem();
        }

        private void UpdateItemAmount()
        { 
            itemAmountLabel.Text = itemAmount.ToString();
        }

        private void LoadItem()
        { 
            itemAmountLabel.BackColor = Color.FromArgb(0, Color.Black);
            inventoryAmount = item.Amount;
            stockLabel.Text = "In stock | only " + inventoryAmount + " pcs.";

            itemNameLabel.Text = item.Name;
            brandLabel.Text = item.Brand;
            infoLabel.Text = item.Info;
            priceLabel.Text = item.Price.ToString() + "€";
            lastForm.DisplayImage(itemPic, item.Name);
        }

        private void UC_Item_Load(object sender, EventArgs e)
        {

        }

        private void leftArrow_Click(object sender, EventArgs e)
        {
            if (itemAmount > 1)
            {
                itemAmount--;
                UpdateItemAmount();
            }
        }

        private void rightArrow_Click(object sender, EventArgs e)
        {
            if (itemAmount < inventoryAmount)
            {
                itemAmount++;
                UpdateItemAmount();
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            CartItem cItem = new CartItem(item, itemAmount);

            if (parentForm.cartItems.Any(x=>x.Cartitem.ItemID == item.ItemID))
            {
                int listIndex = parentForm.cartItems.FindIndex(x => x.Cartitem.ItemID == item.ItemID);
                int totalAmount = parentForm.cartItems[listIndex].Amount + cItem.Amount;
                int availableAmount = parentForm.cartItems[listIndex].Cartitem.Amount;

                if (totalAmount <= availableAmount)
                {
                    parentForm.cartItems[listIndex].Amount = totalAmount;
                }
                else
                {
                    MessageBox.Show("Not enough items in stock");
                }
            }
            else
            {
                parentForm.cartItems.Add(cItem);
            }

            parentForm.cartNotificationIcon.Visible = true;
        }
    }
}
