﻿import {createBrowserRouter, Navigate} from "react-router-dom";
import Layout from "./pages/Layout/Layout";
import ErrorPage from "./pages/ErrorPage";
import HomePage from "./pages/start/HomePage.tsx";
import AboutPage from "./pages/start/AboutPage.tsx";
import BankPage from "./pages/start/BankPage.tsx";
import DashboardLayout from "./pages/Layout/DashboardLayout.tsx";
import FintrackerPage from "./pages/start/FintrackerPage.tsx";
import BudgetsPage from "./pages/budget/BudgetsPage.tsx";
import BudgetDetailsPage from "./pages/budget/BudgetDetailsPage.tsx";
import WalletLayout from "./pages/Layout/WalletLayout.tsx";
import WalletTransactionsPage from "./pages/wallet/WalletTransactionsPage.tsx";
import WalletGeneralSettingsPage from "./pages/wallet/WalletGeneralSettingsPage.tsx";
import WalletCategoriesSettingsPage from "./pages/wallet/WalletCategoriesSettingsPage.tsx";
import RegisterForm from "./components/auth/RegisterForm.tsx";
import LoginForm from "./components/auth/LoginForm.tsx";
import WalletSettingsLayoutPage from "./pages/Layout/WalletSettingsLayoutPage.tsx";
import InviteAccept from "./components/auth/InviteAccept.tsx";
import GlobalSettings from "./pages/Layout/GlobalSettings.tsx";
import AccountSettings from "./pages/AccountSettings.tsx";
import ResetEmail from "./components/auth/ResetEmail.tsx";
import ResetPassword from "./components/auth/ResetPassword.tsx";

const router = createBrowserRouter([
    {
        id: 'root',
        path: '/',
        element: <Layout/>,
        errorElement: <ErrorPage/>,
        children: [
            {
                id: 'home',
                index: true,
                element: <HomePage/>
            },
            {
                id: 'about',
                path: 'about',
                element: <AboutPage/>,
                errorElement: <div>DASDASD</div>
            },
            {
                id: 'bank',
                path: 'bank',
                element: <BankPage/>
            }
        ],

    },
    {
        id: 'dashboard',
        path: 'dashboard',
        element: <DashboardLayout/>,
        children: [
            {
                index: true,
                element: <FintrackerPage/>
            },
            {
                id: 'budgets',
                path: 'budgets',
                element: <BudgetsPage/>
            },
            {
                id: 'budgetDetails',
                path: 'budgets/:budgetId',
                element: <BudgetDetailsPage/>
            }
        ]
    },
    {
        id: 'wallet',
        path: "wallet",
        element: <WalletLayout/>,
        children: [
            {
                index: true,
                element: <Navigate to={"/dashboard"} replace={true}/>
            },
            {
                id: 'walletTransactions',
                path: ":walletId/trans",
                element: <WalletTransactionsPage/>
            },
            {
                id: 'walletBudgets',
                path: ':walletId/budgets',
                element: <BudgetsPage/>
            },
            {
                id: 'walletBudgetDetails',
                path: ':walletId/budgets/:budgetId',
                element: <BudgetDetailsPage/>
            },
            {
                id: 'walletSettings',
                path: ':walletId/settings',
                element: <WalletSettingsLayoutPage/>,
                children: [
                    {
                        index: true,
                        id: 'walletSettingsGeneral',
                        element: <WalletGeneralSettingsPage/>
                    },
                    {
                        id: 'walletSettingsCategories',
                        path: "categories",
                        element: <WalletCategoriesSettingsPage/>
                    }
                ]
            },
        ]
    },
    {
        id: "settings",
        path: 'settings',
        element: <GlobalSettings/>,
        children: [
            {
                id: 'accountSettings',
                index: true,
                element: <AccountSettings/>
            },
            {
                id: 'categoriesSettings',
                path: 'all-categories',
                element: <WalletCategoriesSettingsPage/>
            }
        ]
    },
    {
        id: 'registration',
        path: 'registration',
        element: <RegisterForm/>
    },
    {
        id: 'login',
        path: 'login',
        element: <LoginForm/>
    },
    {
        id: 'confirm-invite',
        path: 'confirm-invite',
        element: <InviteAccept/>
    },
    {
        id: 'email-reset',
        path: 'email-reset',
        element: <ResetEmail/>
    },
    {
        id: 'password-reset',
        path: 'password-reset',
        element: <ResetPassword/>
    }

    
])


export default router; 