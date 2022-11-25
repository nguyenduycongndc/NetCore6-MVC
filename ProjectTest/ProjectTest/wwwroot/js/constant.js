/////// <reference path="customer.js" />
//<reference path="host.js" />
var apiConfig = {
    "api": {
        "host_user_service": hostApi.host_user_service,
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
        "wallet": {
            "controller": "/api/WalletManagement",
            "action": {
                "search": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchWallet"
                },
                "add": {
                    //function support to add item
                    "method": "POST",
                    "path": "/Create"
                },
                "update": {
                    //function support to add item
                    "method": "PUT",
                    "path": "/Update"
                },
                "delete": {
                    //function support to delete item
                    "method": "DELETE",
                    "path": "/Delete"
                },
                "checked": {
                    //function support to active/deactive item
                    "method": "POST",
                    "path": "/Checked"
                },
                "getItem": {
                    "method": "GET",
                    "path": "/Detail"
                },
                "inforWallet": {
                    "method": "POST",
                    "path": "/InforWallet"
                }
            }
        },
        "buys": {
            "controller": "/api/Buys",
            "action": {
                "savedatabuy": {
                    "method": "POST",
                    "path": "/SaveDataBuy"
                },
                "pausedatabuy": {
                    "method": "GET",
                    "path": "/PauseDataBuy"
                }
            }
        },
        "sells": {
            "controller": "/api/Sells",
            "action": {
                "search": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/Search"
                },
                "getItem": {
                    "method": "GET",
                    "path": "/Detail"
                },
                "selectwallet": {
                    "method": "GET",
                    "path": "/SelectWallet"
                },
                "dropdown": {
                    "method": "GET",
                    "path": "/Dropdown"
                },
                "listNTFSell": {
                    "method": "POST",
                    "path": "/ListNTFSell"
                },
                "updateNFT": {
                    "method": "PUT",
                    "path": "/UpdateNFT"
                }
            }
        },
        "statistical": {
            "controller": "/api/Statistical",
            "action": {
                "search": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchStatistical"
                },
                "getItem": {
                    "method": "GET",
                    "path": "/Detail"
                }
            }
        },
        "transaction": {
            "controller": "/api/Transaction",
            "action": {
                "searchhero": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchHero"
                },"searchitem": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchItem"
                },"searchticket": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchTicket"
                },"searchpack": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchPack"
                },"searchegg": {
                    //function support get items by search condition
                    "method": "POST",
                    "path": "/SearchEgg"
                },
                "getItem": {
                    "method": "GET",
                    "path": "/Detail"
                },
                "exportexcelhero": {
                    "method": "GET",
                    "path": "/ExportHero"
                },
                "exportexcelitem": {
                    "method": "GET",
                    "path": "/ExportItem"
                },
                "exportexcelticket": {
                    "method": "GET",
                    "path": "/ExportTicket"
                },
                "exportexcelpack": {
                    "method": "GET",
                    "path": "/ExportPack"
                },
                "exportexcelegg": {
                    "method": "GET",
                    "path": "/ExportEgg"
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

