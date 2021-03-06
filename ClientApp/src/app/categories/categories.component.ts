import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Category } from '../models/Category';
import { AccountService } from '../services/account.service';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>|undefined;
  @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>|undefined;
  @ViewChild('simpleTemplate', {static: false}) simpleTemplate: TemplateRef<any> | undefined;
  
  categories: Category[];
  editedCategory: Category;
  isLoaded: boolean = false;
  statusMessage: string = "";
  isNewRecord: boolean = false;

  constructor(
    private categoryService: CategoryService,    
    private accountService: AccountService
    ) { 
    this.ngOnInit();
  }

  ngOnInit() {
    let subr = this.getCategories();    
  }

  getCategories(){
    return this.categoryService.getCategories()
    .subscribe(
        data => {   
          this.categories = data; 
          this.isLoaded = true;
          console.log(this.categories);
        },
        error => console.log(error)
      );
  }

  isLogged(){
    return this.accountService.isLogged;
  }

  addCategory() {
    this.editedCategory = {id: 0, name: ""};
    this.categories.push(this.editedCategory);
    this.isNewRecord = true;
  }
  
  editCategory(category: Category) {
      this.editedCategory = new Category(category.id, category.name);
  }
  
  loadTemplate(category: Category) {
    if (!this.isLogged()){
      return this.simpleTemplate;  
    }
    else
    {
      if (this.isLogged() && (this.accountService.user.roleName == "admin")){        
        if (this.editedCategory && this.editedCategory.id === category.id) {
          return this.editTemplate;
        } 
        else {
          return this.readOnlyTemplate;
        }    
      } 
      else{
        return this.simpleTemplate; 
      } 
    }  
  }
  
  saveCategory() {
      if (this.isNewRecord) {
          this.categoryService.createCategory(this.editedCategory).subscribe(data => {
              this.statusMessage = 'New category created',
              this.getCategories();
          });
          this.isNewRecord = false;
          this.editedCategory = null;
      } else {
          this.categoryService.updateCategory(this.editedCategory).subscribe(data => {
              this.statusMessage = 'Category updated',
              this.getCategories();
          });
          this.editedCategory = null;
      }
  }
  
  cancel() {
      if (this.isNewRecord) {
          this.categories.pop();
          this.isNewRecord = false;
      }
      this.editedCategory = null;
  }
  
  deleteCategory(category: Category) {
      this.categoryService.deleteCategory(category.id).subscribe(data => {
          this.statusMessage = 'Category deleted',
          this.getCategories();
      });
  }
}
