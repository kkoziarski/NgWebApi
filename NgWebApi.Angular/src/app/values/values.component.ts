import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/dataService';

@Component({
    selector: 'app-values',
    templateUrl: 'values.component.html',
    providers: [
        DataService
    ]
})

export class ValuesComponent implements OnInit {

    public message: string;
    public values: any[];

    constructor(private _dataService: DataService) {

        this.values = [];
        this.message = 'Hello from ValuesComponent ctor';
    }

    ngOnInit(): void {
        this._dataService
        .GetAll()
        .subscribe(data => this.values = data,
                error => console.log(error),
                () => console.log('Get all complete'));
    }
}
