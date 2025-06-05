import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Category } from '../shared/models/category';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';
import { Image } from '../shared/models/image';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'http://localhost:5187/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.categoryId > 0) params = params.append('categoryId', shopParams.categoryId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageIndex);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('search', shopParams.search);


    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'product', { params });
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'product/' + id);
  }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'category')
  }

  // getTypes() {
  //   return this.http.get<Type[]>(this.baseUrl + 'products/types')
  // }

}
