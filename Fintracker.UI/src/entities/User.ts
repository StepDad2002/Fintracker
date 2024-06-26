﻿import { Budget } from "./Budget";
import { UserDetails } from "./UserDetails";
import { Wallet } from "./Wallet";
import {Currency} from "./Currency.ts";

export interface User extends BaseEntity {
    email: string;
    userName: string;
    userDetails: UserDetails;
    avatarBlob: FileList;
    ownedBudgets: Budget[];
    memberBudgets: Budget[];
    memberWallets: Wallet[];
    ownedWallets: Wallet[];
    globalCurrency: Currency;
    currencyId: string;
}