import { Component, OnInit } from '@angular/core';
import { Product } from '../models/Product';
import { HttpService } from '../services/http.service';
@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {

  products: Product[] = [];

  constructor(private httpService: HttpService) { }

  ngOnInit() {
    this.getProducts();
  }

  getProducts(): void{
    this.httpService.GetProducts().subscribe(data => this.products = data)
  }

}
