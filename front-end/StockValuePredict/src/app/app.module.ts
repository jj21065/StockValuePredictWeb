import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ValuePredictMainComponent } from './view/value-predict-main/value-predict-main.component';

import { TableModule } from 'primeng/table';

@NgModule({
  declarations: [AppComponent, ValuePredictMainComponent],
  imports: [BrowserModule, AppRoutingModule, NgbModule, TableModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
