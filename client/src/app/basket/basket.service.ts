import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketItem } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  

  constructor(private http: HttpClient) { }

  getBasket(id: string) {
    return this.http.get<Basket>(this.baseUrl + 'cart' + id).subscribe({
      next: basket => this.basketSource.next(basket)
    })
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(this.baseUrl + 'cart', basket).subscribe({
      next: basket => this.basketSource.next(basket)
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: Product, quantity = 1) {
    const itemToAdd = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
  }
  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const item=items.find(x=>x.productId===itemToAdd.productId);
    if(item)item.quantity+=quantity
    else{
      itemToAdd.quantity=quantity;
      items.push(itemToAdd);
    }
    return items
  }


  createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('cart', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: Product): BasketItem {
    return {
      productId: item.id,
      productName: item.productName,
      price: item.price,
      quantity: 0
    }
  }
}
