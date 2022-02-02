import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/Category';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  productApi : string = 'https://185.152.139.93:443/api/products';

  constructor(
    private http: HttpClient,
  ) { }

  getProducts() { 
    let url = `${this.productApi}/`;   
    return this.http.get<Product[]>(url, this.httpOptions);
  }

  searchProducts(term: string) {
    console.log(term);
    let url = `${this.productApi}/?name=${term}`;   
    return this.http.get<Product[]>(url, this.httpOptions);
  }

  deleteProduct(id: number) {
    let url = `${this.productApi}/${id}`;
    return this.http.delete<Product>(url, this.httpOptions);
  }

  getProduct(id: number) {   
    let url = `${this.productApi}/${id}`; 
    //alert(url);
    return this.http.get<Product>(url, this.httpOptions);
  }  

  createProduct(product: Product){
    let url = `${this.productApi}`;
    return this.http.post<Product>(url, product, this.httpOptions);
  }

  updateProduct(editedProduct: Product) {
    let url = `${this.productApi}/${editedProduct.id}`;
    return this.http.put<Product>(url, editedProduct, this.httpOptions);
  }  
}
