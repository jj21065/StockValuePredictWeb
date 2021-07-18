import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ValuePredictMainComponent } from './view/value-predict-main/value-predict-main.component';

const routes: Routes = [
  { path: 'main-page', component: ValuePredictMainComponent },
  {
    path: '**',
    redirectTo: '/main-page',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
