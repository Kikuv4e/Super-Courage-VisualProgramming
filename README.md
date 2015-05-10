#Super Courage!

	Опис на апликацијата
	
![Опис на апликацијата](http://i.imgur.com/PAc2q72.png)  
Проектот претставува игра во која кучето Courage треба да освои што е можно повеќе парички и да се задржи подолго во облаците. За да преживее треба успешно да ги избегне сите пречки кои вклучуваат бомби кои се активираат на неопределено време и птиците кои повремено се појавуваат. Птиците се појавуваат на случајна висина, додека бомбите се активираат во случаен избор од даден временски интервал.
Играта има само едно ниво, каде најдобар играч ќе биде оној кој ќе освои најмногу поени. 
Кучето се движи со помош на стрелките од тастатура.
![Game Over](http://i.imgur.com/gJSdFEA.png)


##Пресметување на поени:

Паричка: Секоја паричка носи 1000 поени, а со секоја секунда помината во воздух се добиваат поени во вредност од 100 до 300.

##Почетно мени
![Почетно мени](http://i.imgur.com/WMwJrvB.png)
Почетното мени нуди опции за избор на нова игра, приказ на листа со најдобри поени и излез од играта.
## Како да се игра играта
![How to play] (http://i.imgur.com/tN55uz2.png)
Овај прозорец кажува како да се игра дадената игра.
##Форма за најавување
![Форма за најавување](http://i.imgur.com/gSUQbDd.png)
При избор на опцијата за нова игра се појавува форма за најавување во која играчот мора да го внесе своето име.

##Приказ на информации за High score
![Приказ на информации за High score](http://i.imgur.com/kLMBBkz.png)
Со избирање на опцијата High Score се прикажува листа со најдобри играчи.

##Опција за излез 
Со притискање на  опцијата Quit се излегува од играта.

Доколку е стартувана играта, со притискање на копчето Esc играчот се враќа во почетното мени.


##Опис на решение

###Class `Bomb`

Креира објект од класата Bomb, и ја регулира експлозијата.

###Class `Clouds`

Се чуваат податоци за облаците. Креира објект од класата  Clouds каде што веројатноста да се појави бомба на облакот е 20%, а веројатноста да се појави паричка е 30%.
###Class `Player`

Се креира Courage и се дефинира неговото движење и умирање.

###Class `MovingObject `

Сите објекти што се движат наследуваат од оваа класа
###Class `CuteBird`

Создава објект од класата и нејзините движења.

###Class `Money`

Класа во која се чуваат податоци за парите и новно креирање.

###Class `Person`

Класа која се однесува на играчот кој ја игра играта и во која се чуваат неговото име и поени.

## Опис на функцијата checkIfCollide
```csharp
public bool checkIfCollide(Point playerLocation, int playerHeight, int playerWidth, out bool isAlive, ref int score)
        {
            Point left = new Point(playerLocation.X , playerLocation.Y + playerHeight);
            Point right = new Point(playerLocation.X + playerWidth , playerLocation.Y + playerHeight);
            isAlive = true;
            for (int i = 0; i < clouds.Count; i++)
            {
                
                bool t = clouds.ElementAt(i).checkPlayerPosition(left, right, playerHeight, playerWidth, out isAlive, ref score);
                if (t)
                {
                    return true;
                }

            }
            return false;
        }
```
Дадената функција прима 5 аргументи. Првиот аргумент е локацијата на играчот, вториот е висината на играчот, третиот е висината на играчот, четвртиот е да се провери дали е жив уште играчот, и петтиот е да враќа дали нашол одредени парички. На почеток на функцијата креираме две точки, што преставуваат линија кај нозете на играчот. Истата таа линија проверувам дали има пресек со одреден облак. Облакот се содржи од бомби и парички, доколку играчот дојде во допир со паричка на облакот како аргумент преку референца score се враќа во формата gameWindow, и сегашните поени се зголемуваат за 1000. Паричките ги снемува на допир, доколку бомба експлодира во близина на играчот со аргументот isAlive се пренесува назад во формата gameWindow, и се одредува дали играчот преживеал или не. Функцијата враќа boolean променлива што ни кажува дали играчот нашол одреден облак на кој може да застане, со кој му наредува на играчот да престане да паѓа.
## Изработиле

##131503 Љубица Коцева
##131510 Ана Бошковска
##131534 Кире Колароски
