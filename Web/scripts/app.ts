import {Component} from '@angular/core';
import {Routes, ROUTER_DIRECTIVES} from '@angular/router';
import {ViewComponent} from './view/view.component';

@Component({
    selector: 'my-app',
    template:
    '<h3>My Second Angular 2 App</h3>' +
    '<ul>' +
    '<li *ng-for="#resource if resources"><a [router-link]="['view', {name: resource.name}]">{{resource.name}}</a>',
    directives: [ROUTER_DIRECTIVES],
})

@Routes([
    { path: '/view', component: ViewComponent },
])



export class AppComponent { }