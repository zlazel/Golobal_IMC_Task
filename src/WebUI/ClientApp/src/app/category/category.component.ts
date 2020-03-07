import { Component, TemplateRef } from '@angular/core';
import { ProductsClient, CreateProductCommand, ProductDto,
     UpdateProductCommand, CategoriesVm, CategoriesClient,
      CategoryDto, CreateCategoryCommand, UpdateCategoryCommand } from '../golobal_imc_task-api';
import { faPlus, faEllipsisH } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { UpdateProductDetailCommand } from '../cleanarchitecture-api';

@Component({
    selector: 'app-category-component',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
export class CategoryComponent {

    debug: boolean;

    vm: CategoriesVm;

    selectedList: CategoryDto;
    selectedItem: ProductDto;
    newProduct= new ProductDto();
    
    newListEditor: any = {};
    listOptionsEditor: any = {};
    itemDetailsEditor: any = {};

    newListModalRef: BsModalRef;
    listOptionsModalRef: BsModalRef;
    deleteListModalRef: BsModalRef;
    itemDetailsModalRef: BsModalRef;
    newProductModalRef: BsModalRef;

    faPlus = faPlus;
    faEllipsisH = faEllipsisH;

    constructor(
        private listsClient: CategoriesClient,
         private itemsClient: ProductsClient,
          private modalService: BsModalService
          ) {
        listsClient.get().subscribe(
            result => {
                this.vm = result;
                if (this.vm.categories.length) {
                    this.selectedList = this.vm.categories[0];
                }
            },
            error => console.error(error)
        );
    }

    // Lists
    remainingItems(list: CategoryDto): number {
        return list.products.length;
    }

    showNewListModal(template: TemplateRef<any>): void {
        this.newListModalRef = this.modalService.show(template);
        setTimeout(() => document.getElementById("title").focus(), 250);
    }
    
    newListCancelled(): void {
        this.newListModalRef.hide();
        this.newListEditor = {};
    }

    addList(): void {
        let list = CategoryDto.fromJS({
            id: 0,
            categoryName: this.newListEditor.title,
            products: [],
        });

        this.listsClient.create(<CreateCategoryCommand>{ categoryName: this.newListEditor.title }).subscribe(
            result => {
                list.id = result;
                this.vm.categories.push(list);
                this.selectedList = list;
                this.newListModalRef.hide();
                this.newListEditor = {};
            },
            error => {
                let errors = JSON.parse(error.response);

                if (errors && errors.Title) {
                    this.newListEditor.error = errors.Title[0];
                }

                setTimeout(() => document.getElementById("title").focus(), 250);
            }
        );
    }

    showListOptionsModal(template: TemplateRef<any>) {
        this.listOptionsEditor = {
            id: this.selectedList.id,
            categoryName: this.selectedList.categoryName,
        };

        this.listOptionsModalRef = this.modalService.show(template);
    }

    updateListOptions() {
        console.log('this.selectedList');
        console.log(this.selectedList);
        
        this.listsClient.update(this.selectedList.id, UpdateCategoryCommand.fromJS(this.listOptionsEditor))
            .subscribe(
                () => {
                    this.selectedList.categoryName = this.listOptionsEditor.categoryName,
                    this.listOptionsModalRef.hide();
                    this.listOptionsEditor = {};
                },
                error => console.error(error)
            );
    }

    confirmDeleteList(template: TemplateRef<any>) {
        this.listOptionsModalRef.hide();
        this.deleteListModalRef = this.modalService.show(template);
    }

    deleteListConfirmed(): void {
        this.listsClient.delete(this.selectedList.id).subscribe(
            () => {
                this.deleteListModalRef.hide();
                this.vm.categories = this.vm.categories.filter(t => t.id != this.selectedList.id)
                this.selectedList = this.vm.categories.length ? this.vm.categories[0] : null;
            },
            error => console.error(error)
        );
    }

    // Items

    showItemDetailsModal(template: TemplateRef<any>, item: ProductDto): void {
        this.selectedItem = item;
        this.itemDetailsEditor = {
            ...this.selectedItem
        };

        this.itemDetailsModalRef = this.modalService.show(template);
    }

    showNewProductModal(template: TemplateRef<any>, item: ProductDto): void {
        this.selectedItem = item;
        this.itemDetailsEditor = {
            ...this.selectedItem
        };

        this.newProductModalRef = this.modalService.show(template);
    }

    updateItemDetails(): void {
        this.itemDetailsEditor = {
            ...this.selectedItem
        };
        this.itemsClient.update(this.selectedItem.id, UpdateProductCommand.fromJS(this.itemDetailsEditor))
            .subscribe(
                () => {
                    
                    if (this.selectedItem.categoryId != this.itemDetailsEditor.CategoryId) {
                        this.selectedList.products = this.selectedList.products.filter(i => i.id != this.selectedItem.id)
                        let listIndex = this.vm.categories.findIndex(l => l.id == this.itemDetailsEditor.categoryId);
                        this.selectedItem.categoryId = this.itemDetailsEditor.categoryId;
                        this.vm.categories[listIndex].products.push(this.selectedItem);
                    }

                    this.selectedItem.price = this.itemDetailsEditor.price;
                    this.selectedItem.title = this.itemDetailsEditor.title;
                    this.itemDetailsModalRef.hide();
                    this.itemDetailsEditor = {};
                },
                error => console.error(error)
            );
    }
    createItemDetails(): void {
        this.newProduct.categoryId = this.selectedList.id;
        this.itemDetailsEditor = {
            ...this.newProduct
        };
        
        this.itemsClient.create(CreateProductCommand.fromJS(this.itemDetailsEditor))
            .subscribe(
                () => {
                    
                    if (this.selectedItem.categoryId != this.itemDetailsEditor.CategoryId) {
                        this.selectedList.products = this.selectedList.products.filter(i => i.id != this.selectedItem.id)
                        this.selectedItem.categoryId = this.itemDetailsEditor.categoryId;
                        let listIndex = this.vm.categories.findIndex(l => l.id == this.itemDetailsEditor.categoryId);
                        this.vm.categories[listIndex].products.push(this.newProduct);
                    }

                    this.selectedItem.price = this.itemDetailsEditor.price;
                    this.selectedItem.title = this.itemDetailsEditor.title;
                    this.newProductModalRef.hide();
                    this.itemDetailsEditor = {};
                    this.newProduct = new ProductDto();
                },
                error => console.error(error)
            );
    }
    addItem() {
        let item = ProductDto.fromJS({
            id: 0,
            CategoryId: this.selectedList.id,
            //priority: this.vm.priorityLevels[0].value,
            title: '',
            price: 0
            // done: false
        });
        
        this.selectedList.products.push(item);
        let index = this.selectedList.products.length - 1;
        this.editItem(item, 'itemTitle' + index);
    }

    editItem(item: ProductDto, inputId: string): void {
        this.selectedItem = item;
        setTimeout(() => document.getElementById(inputId).focus(), 100);
    }

    updateItem(item: ProductDto, pressedEnter: boolean = false): void {
        let isNewItem = item.id == 0;

        if (!item.title.trim()) {
            this.deleteItem(item);
            return;
        }

        if (item.id == 0) {
            this.itemsClient.create(CreateProductCommand.fromJS({ ...item, CategoryId: this.selectedList.id }))
                .subscribe(
                    result => {
                        item.id = result;
                    },
                    error => console.error(error)
                );
        } else {
            this.itemsClient.update(item.id, UpdateProductCommand.fromJS(item))
                .subscribe(
                    () => console.log('Update succeeded.'),
                    error => console.error(error)
                );
        }

        this.selectedItem = null;

        if (isNewItem && pressedEnter) {
            this.addItem();
        }
    }

    // Delete item
    deleteItem(item: ProductDto) {
        if (this.itemDetailsModalRef) {
            this.itemDetailsModalRef.hide();
        }

        if (item.id == 0) {
            let itemIndex = this.selectedList.products.indexOf(this.selectedItem);
            this.selectedList.products.splice(itemIndex, 1);
        } else {
            this.itemsClient.delete(item.id).subscribe(
                () => this.selectedList.products = this.selectedList.products.filter(t => t.id != item.id),
                error => console.error(error)
            );
        }
    }
}
