import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormsModule } from '@angular/forms';
import { Product } from '../models/Product';
import { ProductService } from '../services/product.service';
import { Category } from '../models/Category';
import { CategoryService } from '../services/category.service';
import { Observable, pipe, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any> | undefined;
  @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any> | undefined;
  @ViewChild('simpleTemplate', {static: false}) simpleTemplate: TemplateRef<any> | undefined;
  
  toppings = new FormControl();
  products: Product[] = [];
  filteredProducts: Product[] = [];
  categories: Category[] = [];
  selectedCategories: boolean[] = [];
  selectedCategory: Category;
  isLoaded: boolean = false;
  editedProduct: Product;
  isNewRecord: boolean = false;
  statusMessage: string = "";
  products$!: Observable<Product[]>;
  private searchTerms = new Subject<string>();

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private accountService: AccountService
    ) { 
    //this.ngOnInit();
  }

  ngOnInit() {
    this.getProducts();    
    this.getCategories();
    this.accountService.getAccountInfo();
  }

  search(term: string): void {
    this.searchTerms.next(term);
    this.productService.searchProducts(term).subscribe(
      data => {
        this.products = data;
        this.filterProducts();
      });
  }

  filterProducts(){
    let flag = false;
    for (let i = 0; i < this.categories.length; i++){
      if (this.selectedCategories[this.categories[i].name] === true){
        flag = true;
        break;
      }
    }
    
    if (flag) {
      console.log('filter on');
      this.filteredProducts = this.products.filter(value => this.selectedCategories[value.categoryName] === true)
    }
    else{
      console.log('filter off');
      this.filteredProducts = this.products;
    }
  }

  isLogged(){
    return this.accountService.isLogged;
  }

  getCategories(){
    return this.categoryService.getCategories()
    .subscribe(
        data => {   
          this.categories = data;
          console.log(this.categories);
        },
        error => console.log(error)
      );
  }

  getProducts(){
    return this.productService.getProducts()
    .subscribe(
        data => {   
          this.products = data;
          this.isLoaded = true;
          this.filterProducts();
          console.log(this.products);
        },
        error => console.log(error)
      );
  }

  addProduct() {
    this.editedProduct = 
    {
      id: 0,
      name: "",
      categoryName: "",
      description: "",
      price: 0,
      specification: "",
      specialSpec: ""
    };
    this.products.push(this.editedProduct);
    this.isNewRecord = true;
  }
  
  editProduct(product: Product) {
    this.editedProduct = Object.assign({}, product) as Product;
  }

  loadTemplate(product: Product) {
    if (!this.isLogged()){
      return this.simpleTemplate;  
    }
    else
    {
      if (this.isLogged() && (this.accountService.user.roleName == "user" || this.accountService.user.roleName == "admin")){
        if (this.editedProduct && this.editedProduct.id === product.id) {
          return this.editTemplate;
        } else {
          return this.readOnlyTemplate;
        }    
      }  
    }           
  }
  
  saveProduct() {
      if (this.isNewRecord) {
          this.productService.createProduct(this.editedProduct).subscribe(data => {
              this.statusMessage = 'Product is added',
              this.getProducts();
          });
          this.isNewRecord = false;
          this.editedProduct = null;
      } else {        
          this.productService.updateProduct(this.editedProduct).subscribe(data => {
              this.statusMessage = 'Producted is updated',
              this.getProducts();
          });
          this.editedProduct = null;
      }
  }
  
  cancel() {    
      if (this.isNewRecord) {
          this.products.pop();
          this.isNewRecord = false;
      }
      this.editedProduct = null;
  }

  onChange(category: Category){
    if(this.selectedCategories[category.name] === true){
      this.selectedCategories[category.name] = false;
    }
    else{
      this.selectedCategories[category.name] = true;
    }    
    
    this.filterProducts();
  }
  
  deleteProduct(Product: Product) {
      this.productService.deleteProduct(Product.id).subscribe(data => {
          this.statusMessage = 'Product is deleted',
          this.getProducts();
      });
  }
}

