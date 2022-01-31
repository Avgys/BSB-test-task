import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/Product';
import { Site } from '../models/Site';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  productApi : string = 'https://localhost:5001/api/Products';

  constructor(
    private http: HttpClient,
  ) { }

  GetProducts() { 
    let url = `${this.productApi}/`;   
    return this.http.get<Product[]>(url, this.httpOptions);
  }

  GetProduct(id: number) {   
    let url = `${this.productApi}/${id}`; 
    //alert(url);
    return this.http.get<Site[]>(url, this.httpOptions);
  }
}
