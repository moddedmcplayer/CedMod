Master Branch: ![.NET Core](https://github.com/CedModV2/CedMod/workflows/.NET%20Core%20Master/badge.svg?branch=master)

Dev Branch: ![.NET Core](https://github.com/CedModV2/CedMod/workflows/.NET%20Core%20Dev/badge.svg?branch=master)

Get the latest stable build here: https://github.com/CedModV2/CedMod/actions?query=branch%3Amaster (click on the most top one that has a checkmark and download the artifact named CedMod Build Results)


Get the latest dev build here: https://github.com/CedModV2/CedMod/actions?query=branch%3Adev (click on the most top one that has a checkmark and download the artifact named CedMod Build Results)

(we also have modified versions of some of jokers gamemodes :))
Configs go in config_gameplay.txt
| config                              | type      | default  | description                                                |
|-------------------------------------|----------:|:--------:|:----------------------------------------------------------:|
| cm_nicknamefilter                   |    list   |          | words in here will be changed to BOBBA (filtered word)     |
| bansystem_banappealurl              |   string  | none     | The URL banned players will see along with the ban details |
| cm_customloadingscreen              |   bool    | true     | If the custom loading screen is enabled                    |
| bansystem_geo                       |   list    |          | If GEO block is enabled                                    |
| bansystem_apikey                    |   string  | none     | The API key                                                |
| bansystem_alias                     |   string  | none     | The alias Aka indentifier is associated with the API key   |
| bansystem_webhook                   |   string  | none     | The webhook url messages regarding bans will be sent to    |
| ffa_dclassvsdclasstk                |   bool    | false    | If d class vs d class tks count for FFA                    |
| ffa_killingdisarmedclassdsallowed   |   bool    | false    | If teamkilling disarmed d class counts for FFA             |
| ffa_killingdisarmedscientistallowed |   bool    | false    | If teamkilling disarmed scientists counts for FFA          |
| ffa_enable                          |   bool    | true     | If FFA is enabled                                          |
| ffa_ammountoftkbeforeban            |   int     | 3        | How many teamkills you can do before the hammer            |
| ffa_banduration                     |   int     | 4320     | How long FFA bans last in minutes                          |
