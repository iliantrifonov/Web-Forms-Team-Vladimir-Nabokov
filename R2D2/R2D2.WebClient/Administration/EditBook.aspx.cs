﻿using R2D2.Data;
using R2D2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace R2D2.WebClient.Administration
{
    public partial class EditBook : System.Web.UI.Page
    {
        private IData data;

        public EditBook(IData data)
        {
            this.data = data;
        }

        public EditBook()
            : this(new BooksData())
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //var categoryBox = this.FvEdit.FindControl("chlCategories") as CheckBoxList;
            //var book = this.data.Books.Find(Request.QueryString["id"]);
            //foreach (var cat in book.Categories)
            //{
            //    var stuff = categoryBox.Items.Cast<ListItem>().FirstOrDefault(li => int.Parse(li.Value) == cat.Id);
            //    stuff.Selected = true;
            //}

            //categoryBox.DataBind();
        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public Book FvEdit_GetItem([QueryString]Guid id)
        {
            var book = this.data.Books.Find(id);
            return book;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void FvEdit_UpdateItem(Guid id)
        {
            var item = this.data.Books.Find(id);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                this.data.SaveChanges();
            }

            Response.Redirect("~/Administration/EditBook.aspx?id=" + id.ToString());
        }

        public IQueryable<Category> DdlCategories_GetData()
        {
            return this.data.Categories.All();
        }
    }
}