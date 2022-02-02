import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  
  
  categoryApi : string = 'https://185.152.139.93:443/api/category';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getCategories() { 
    let url = `${this.categoryApi}/`;   
    return this.http.get<Category[]>(url, this.httpOptions);
  }

  getCategory(id: number) {   
    let url = `${this.categoryApi}/${id}`; 
    return this.http.get<Category>(url, this.httpOptions);
  }

  deleteCategory(id: number) {
    let url = `${this.categoryApi}/${id}`;
    return this.http.delete<Category>(url, this.httpOptions);
  } 

  createCategory(category: Category){
    let url = `${this.categoryApi}`;
    return this.http.post<Category>(url, category, this.httpOptions);
  }

  updateCategory(category: Category) {
    let url = `${this.categoryApi}/${category.id}`;
    return this.http.put<Category>(url, category, this.httpOptions);
  }
}
