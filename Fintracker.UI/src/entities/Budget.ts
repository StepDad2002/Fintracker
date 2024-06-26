﻿import { Category } from "./Category.ts";
import { FinancialEntity } from "./FinancialEntity.ts";
import { Transaction } from "./Transaction.ts";
import { User } from "./User.ts";
import {Wallet} from "./Wallet.ts";
import {RegisterOptions} from "react-hook-form";

export interface Budget extends FinancialEntity {
    categories: Category[];
    categoryIds: string[]
    startDate: string;
    endDate: string;
    ownerId: string;
    isPublic: boolean;
    owner: User;
    members: User[];
    wallet: Wallet;
    walletId: string;
    transactions: Transaction[];
}

export const nameRegisterOptionsForBudget: RegisterOptions = {
    maxLength: {value: 50, message: "Maximum name for budget is 50"},
    minLength: {value: 3, message: "Minimum length for budget name is 3"},
    required: "Name for budget is required",
}

export const balanceRegisterOptionsForBudget: RegisterOptions = {
    valueAsNumber: true,
    max: {value: 100_000_000_000, message: "Maximum balance for budget is 100 billions"},
    required: 'Balance is required'
}

export const startDateRegisterOptionsForBudget : RegisterOptions = {
    required: "Start date for budget is required",
    validate: (value, formValues) => {
        const startDate = new Date(value).toLocaleString('en-CA');
        const endDate = new Date(formValues.endDate).toLocaleString('en-CA');
        
        if(startDate > endDate)
            return "Start date can not be greater than end date"
        return true;
    }
}

export const endDateRegisterOptionsForBudget : RegisterOptions = {
    required: "End date for budget is required",
    validate: (value, formValues) => {
        const endDate = new Date(value).toLocaleString('en-CA');
        const startDate = new Date(formValues.startDate).toLocaleString('en-CA');
        if(endDate < startDate)
            return "End date can not be less than start date"
        if(endDate === startDate)
            return "End date can't be same as start date"
        return true;
    }
}