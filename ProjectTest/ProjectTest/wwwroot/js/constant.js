/////// <reference path="customer.js" />
//<reference path="host.js" />
var apiConfig = {
    "api": {
        "host_user_service": hostApi.host_user_service,
        "login": {
            "controller": "",
            "action": {
                "sendotp": {
                    "method": "POST",
                    "path": "/SendOTP"
                },
                "forgotpassword": {
                    "method": "POST",
                    "path": "/ForgotPassWord"
                }
            }
        },
        "user": {
            "controller": "/api/User",
            "action": {
                "getItem": {
                    "method": "GET",
                    "path": "/Detail"
                },
                "search": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/Search"
                },
                "add": {
                    //function support to add item
                    "method": "POST",
                    "path": "/Create"
                },
                "delete": {
                    //function support to delete item
                    "method": "DELETE",
                    "path": "/Delete"
                },
                "update": {
                    //function support to update item
                    "method": "PUT",
                    "path": "/Update"
                },
                "changepassword": {
                    //function support to update item
                    "method": "PUT",
                    "path": "/ChangePassWord"
                }
            }
        },
        "account": {
            "controller": "/api/Account",
            "action": {
                "searchaccount": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccount"
                },"searchaccounthero": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountHero"
                },"searchaccountitem": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountItem"
                },"searchaccountticket": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountTicket"
                },"searchaccountpack": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountPack"
                },"searchaccountegg": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountEgg"
                },"searchaccountshard": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountShard"
                },"searchaccountref": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchAccountRef"
                },
                "getItem": {
                    "method": "GET",
                    "path": "/Detail"
                }
            }
        }
    },
};

