import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';

import { AppComponent } from './app.component';

import { DatePipe, DecimalPipe, PercentPipe } from '@angular/common';
import { ValuePredictMainComponent } from './view/value-predict-main/value-predict-main.component';

@NgModule({
  declarations: [AppComponent, ValuePredictMainComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    SharedModule,
  ],
  providers: [DatePipe, DecimalPipe, PercentPipe],
  bootstrap: [AppComponent],
})
export class AppModule {}
