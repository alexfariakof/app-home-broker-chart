import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ChartService } from '../../shared/services';
import { AcoesComponent } from './acoes.component';

@NgModule({
  declarations: [AcoesComponent],
  imports: [ BrowserModule,CommonModule, HttpClientModule],
  providers: [ChartService],
  exports: [AcoesComponent]
})

export class Acoesodule { }
