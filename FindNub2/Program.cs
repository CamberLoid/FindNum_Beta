using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Timers;
using System.Linq;
public class NumberGuess
{
    static string programPath = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\NumberGuess");
    static int state = -1; //存储游戏所代表的状态
    //int max, min;
    //int currentNumber;
    public NumberGuess()
    {
        //StreamReader load;
        //StreamWriter save;
        try
        {
            queryInit();Play();
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("Exception:你好像进入了未开发的地方");
            state = 99;
            saveGame();
        }
        finally
        {
            Console.WriteLine("按任意键继续");
            Console.ReadKey();
        }
        //state = Convert.ToInt32(save.ReadByte());
        

    }

    void Play()
    {
        switch (state) //开始判断存档状态
        {
            case -1:
                Console.WriteLine("你不该看到这个错误\n你肯定干了什么坏事");
                throw new NotImplementedException();
            case 0:
                File.Delete(programPath);
                break;
            case 1:
                phase1Guess();
                break;
            case 2:
                phase2Guess();
                break;
            case 3:
                phase3Guess();
                break;
            case 4:
                phase4Guess();
                break;
            case 99:
                bonusPhase();
                break;
            case 100:
                choosePhase();
                break;
            default:
                Console.WriteLine(
                    @"UnknownError");
                break;
        }
        //throw new NotImplementedException();
    }

    void phase4Guess()
    {
        int enemyHP = 10000, allyHP = 2000;
        int enemyCombo = 1, allyCombo = 1;
        Random random = new Random();
        string readin;
        int rndNumber = random.Next(1, 100);
        int currentstate = 0, tryState = 0;
        int[] prime = new int[] {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59
        ,61,67,71,73,79,83,89,97};
        bool isPrime = prime.Contains(rndNumber);
        SpecialWrite.SleepWrite(ConsoleColor.White,
@"现在是天启历2022年，距离地球舰队和三体人宣战已经过了3000年
借着银河碳基联邦的先进科技，地球获得了一个强大的武器：奇点炸弹
距离最终决战还有17个恒星时，地球舰队正在准备组织进攻
奇点炸弹的发射装置需要准备由奇点炸弹的核心定义的攻击序列，以使得奇点炸弹在正确的时间和地点引爆。攻击序列的获取需要不断对奇点炸弹的核心进行询问，有如下两种指令：
",      50);
        SpecialWrite.SleepWrite(ConsoleColor.Green, "Query ", 50);
        SpecialWrite.SleepWrite(ConsoleColor.White, "Intenger\n", 50);
        SpecialWrite.SleepWrite(ConsoleColor.Green, "Answer ", 50);
        SpecialWrite.SleepWrite(ConsoleColor.White, "Bool\n", 50);
        SpecialWrite.SleepWrite(ConsoleColor.White,
@"
其中Query返回攻击序列能否被Number整除
而Answer 1/0 的1代表这个奇点炸弹的序列是素数，0代表不是；错误的回答将会导致奇点炸弹无法正常引爆。
不出意外，人类舰队的旗舰攻击AI，由于无法修复的BUG(由10000年前印度人的后裔制作)偶尔会罢工，以至于人类无法正常组织进攻。
作为指挥官，你确保了其他东西万无一失，但唯独漏了这个。
我们认为对方舰队的HP为10000，出于技术原因，我们舰队的HP为2000
每个攻击序列为素数的引爆都会使得combo+1，造成1000*combo的伤害
每个攻击序列不是素数的引爆，会造成200*(combo+2)的伤害
如果攻击序列判断错误，或者在15次之内没有给给出答案，我们就会因为时间的浪费，而失去100*enemyCombo++点HP，此时奇点炸弹的序列会重设。
你现在需要输入你的指令。
。                       
。                       
",      50);
        while (enemyHP > 0)
        {
            if (tryState == 15)
            {
                Console.WriteLine("你被击中了");
                allyHP -= 100 * (enemyCombo++);
                tryState = 0;
                rndNumber = random.Next(1, 100);
                isPrime = prime.Contains(rndNumber);
                Console.WriteLine($"你剩余HP {allyHP},你的combo {allyCombo},敌方剩余HP {enemyHP},敌方Combo {enemyCombo},");
            }
            SpecialWrite.Write(ConsoleColor.DarkGreen, "等待下一条指令......");
            readin = Console.ReadLine();
            if (readin.ToLower().Contains("query"))
            {
                readin = readin.Remove(0, 6);
                if (int.TryParse(readin, out currentstate))
                {
                    if (currentstate == 0) SpecialWrite.Write(ConsoleColor.DarkMagenta, "False\n");
                    else if (rndNumber % currentstate == 0)
                        SpecialWrite.Write(ConsoleColor.DarkBlue, "True\n");
                    else SpecialWrite.Write(ConsoleColor.DarkMagenta, "False\n"); 
                    tryState++;
                }
                else Console.WriteLine("SystemInterface:参数有误，请重试");
            }
            else if (readin.ToLower().Contains("answer"))
            {
                readin = readin.Remove(0, 7);
                if (int.TryParse(readin, out currentstate))
                {
                    if(isPrime&&currentstate==1||!isPrime&&currentstate==0)
                    {
                        if (isPrime) enemyHP -= 1000 * allyCombo++;
                        else enemyHP -= 200 * (allyCombo + 2);
                        enemyCombo = 1;
                        Console.Write($"命中！ 你的combo {allyCombo},敌方剩余HP {enemyHP}");
                        rndNumber = random.Next(1, 100);
                        isPrime = prime.Contains(rndNumber);
                        tryState = 0;
                    }
                    else
                    {
                        SpecialWrite.Write(ConsoleColor.DarkRed,"我们未能穿透他们的装甲！\n我们被击中了!\n");
                        allyHP -= 100 * (enemyCombo++);
                        rndNumber = random.Next(1, 100);
                        isPrime = prime.Contains(rndNumber);
                        Console.WriteLine($"你剩余HP {allyHP},你的combo {allyCombo},敌方剩余HP {enemyHP},敌方Combo {enemyCombo},");
                        tryState = 0;
                    }
                    if (enemyHP <= 0)
                    {
                        Console.WriteLine("胜利！敌方舰队灰飞烟灭");
                        state = 99;
                        saveGame();
                        return;
                    }
                    if (allyHP <= 0)
                    {
                        Console.WriteLine("我们失败了，快撤！\n世界正在重置中...");
                        //删存档重来
                        state = 0;
                        saveGame();
                        return;
                    }

                }
                else Console.WriteLine("SystemInterface:参数有误，请重试");
            }
            else Console.WriteLine("SystemInterface:指令有误，请重试");
            
        }
    }

    void choosePhase()
    {
        throw new NotImplementedException();
        state = 5;
        saveGame();
        return;
    }

    void phase1Guess()
    {
        string readin;
        string FUDU = "现在，请你给我你的第一次猜测";//复读
        //bool isOver = false;
        Console.Clear();//开始清屏
        int randomNumber = new Random().Next(1, 100);//随机数生成
        int currentState;//存储Parse后当前输入数据
        int i = 0;
        Console.Write(@"8102年
银河碳基联邦的某些成员出于好奇心，再次来到了编号为500921473的蓝色星球，也就是现在人类所知的地球
尽管人类文明评级为5B级，但是，这个星球上面的智能，并不全部都达到了5B水平
这些碳基联邦的智能为了更好的了解人类的本质，正在进行神秘的操作

他们通过某些人类的便携计算设备，也就是电脑，
用一种人类无法察觉的电磁波，解析了这个星球上计算机的计算核心和运行在计算核心上面的交互层，
也就是芯片和操作系统，生成了一个程序，随机放到了某些人的计算设备上。
然后，有一天，你打开了这个程序……
---------------------------------------------------------------------------------------
你普通地错过了闹钟，从六点一路睡到了九点才起了床，发现自己已经完美的错过了第一节课的点名。
你感到很后悔，但同时又感到狂喜，因为你花了几天几夜时间写出来的点名数据篡改的软件已经成功挂在了“学在X电”的服务器。为了方便操作，你还写了个GUI来方便编辑。
");
        
        SpecialWrite.Write(ConsoleColor.Yellow, "Stannum ~");
        SpecialWrite.SleepWriteLine("python home\\Stannum\\ProjectDDUHack\\guiHack.py");
        System.Threading.Thread.Sleep(200);
        //Timer time = new Timer(300);
        //上面那个Timer类死活没搞懂
        Console.WriteLine("摁下回车");
        Console.WriteLine("然而，设计好的GUI并没有出现。你决定新开终端，输入ps，回车");
        Console.WriteLine("突然，你的程序抛出了个异常。有个声音在你脑海中回荡");
        Console.WriteLine("你立马知道了你脑海中的智能要你做什么\n");
        Console.WriteLine("现在你的CPU生成了个随机整数，这个数大于等于1小于等于100，你需要在你的计算机上输入你对这个数字的值的猜测");
        Console.WriteLine("你的计算机的屏幕上出现了以下字符");
        Console.WriteLine("“现在，请你给我你的第一次猜测”");
        readin = Console.ReadLine();
        
        //复读结局入口
        while (readin.Equals(FUDU))
        {
            Console.WriteLine("你输入的数据是非法的，请重新输入：");
            readin = Console.ReadLine();
            FUDU = "你输入的数据是非法的，请重新输入：";
            if (!readin.Equals(FUDU))
            {
                Console.WriteLine(FUDU);
                readin = Console.ReadLine();
                //Console.WriteLine(FUDU);
                break;
            }
            i++;
            if (i >= 10)
            {
                Console.WriteLine(@"
银河碳基联邦的研究员看了看结果，嘴角浮起一丝微笑，像是发现了什么重要的本质......
------------------------------------------------------------------
你的记忆被清空了，GUI正常运行了起来 。。。。。。
又是一个肥宅快乐日啊~~~你心里想到");
                saveGame();
                state = 99;
                //Console.ReadKey();
                //bonusPhase();
                return;
            }
        }
        FUDU = "你输入的数据是非法的，请重新输入：";
        while (true)
        {
            i++;
            if(!int.TryParse(readin,out currentState))
            {
                Console.WriteLine(FUDU);
                i++;
            }
            else if(currentState > randomNumber)
            {
                Console.WriteLine("太大了，下一个");
            }
            else if(currentState == randomNumber)
            {
                Console.WriteLine($@"“这个人类用了{i}次机会啊......”研究员说
“又有数据传过来了......联同思考数据一起传送到母星吧，日后研究。”
------------------------------------------------------------------
你的记忆被清空了，GUI正常运行了起来 。。。。。。
又是一个肥宅快乐日啊~~~你心里想");
                break;
            }
            else
            {
                Console.WriteLine("太小了，下一个");
            }
            readin = Console.ReadLine();
        }
        state = 2;
        saveGame();
        return;
    }
    class P2Item
    {
        public int xmp, us, jarvis, hack;
        public P2Item()
        {
            xmp = 0;us = 0;jarvis = 0;hack = 2;
        }

    }
    void phase2Guess()
    {
        Random random = new Random();
        int targetNumber = random.Next(1,100);
        int tryState = 0; //尝试的次数
        int isHacked = 0; //被干扰的次数，为了照顾某些非洲人，当 ==2时不会被再次干扰
        int currentState = 70; //干扰的概率
        int current;//忘了
        bool isTrue = false;//是否被干扰
        string readin;//读入字符串
        string refuse = "NO";//想再做个分支来着
        P2Item item = new P2Item();
        {
            Console.Clear();
            Console.WriteLine("8102年，宇宙碳基联邦来到地球检验人类发展程度，结果一切良好，认为可以继续存活上千年");
            Console.WriteLine("然而，他们的探测记录被地球三体组织的…你，发现了");
            Console.WriteLine("你认为这是一个绝佳的传递信息的机会，于是拿出了手上的Scanner，来到了离你最近的Portal，意图透过碳基联邦留下的Portal网络向三体人传播信息。");
            Console.WriteLine("要利用银河碳基联邦的强大的广播系统，你必须要使用一系列密钥来Hack进去");
            Console.WriteLine("利用你手上Scanner在加上LAWSON Core强大的Exotic Matter能量供给，你成功破解了他们独有的Glyph加密系统。");
            Console.WriteLine("你将他们的加密系统简化，只需要通过猜测数字，就能成功将你的数据流合法地传送出去。");
            Console.WriteLine("不料，经过Portal的信息流被意图反抗三体人的一群人所监视，我们称他们为反抗军(Resistance)。在反抗军的Control Field内，你发出去的每一比特信息，都由70 % 的概率被反抗军的AEGIS SHEILD干扰，使得你与三体人主机的公钥交换被扰乱，无效化。");
            Console.WriteLine("你只能通过下一次三体人给你反馈的信息得知，你上一次的信息是否合法。");
            Console.WriteLine("经过爆肝，你已经成功把三体人的密钥锁定在了3148401 与 3148500之间，为了简化，取1 - 100");
            Console.WriteLine("现在你还有10次机会和三体人交换密钥信息。如果超过了10次，三体人会将你视为不可信探员，拒绝联络；你的密钥也将会被反抗军所破解，使得地球三体组织的计划败露。");
            SpecialWrite.Write(ConsoleColor.DarkRed, "你开始了最后的尝试……\n");
        }
        while (tryState <=10)
        {
            SpecialWrite.Write(ConsoleColor.Green, "等待下一条指令");
            readin = Console.ReadLine();
            int rnd = random.Next(1,100);//读入，生成随机状态
            switch (readin.ToLower())
            {
                case "hack": //想要有道具功能，但并没有实现数量，可以直接输入使用
                    Console.WriteLine("你觉得在进行下一步行动之前要先hack一遍这个portal");
                    if (item.hack > 0)
                    {
                        item.hack--;
                        if (rnd > 98) { item.jarvis++; Console.WriteLine("Jarvis Virus ++\n你大概是欧皇，居然hack到了Jarvis Virus"); }
                        else if(rnd > 95){ item.us++; Console.WriteLine("L8 UltraStrike ++"); }
                        else if (rnd>70) { item.xmp++; Console.WriteLine("L5 XMP++");}
                        else { Console.WriteLine("然鹅什么也没有发生"); }
                    }
                    else
                    {
                        Console.WriteLine("Portal烧毁，等待4小时重置");
                    }
                    continue;
                case "xmp":
                    if (item.xmp == 0) continue;
                    Console.Write(@"
一个小小的XMP
你看着你的库存 像一个肥宅一样哭了起来
要XMP有什么用吗，又没有办法破除盾的保护。。。
然而但你抛出一个Lv7的XMP时，你意外的发现那个盾的1/4不见了
你开心的想用下一个，可是身为非洲人的你，剩下142个XMP全部没有对那个Shield造成一点伤害
你又像一个肥宅一样哭了起来
");
                    SpecialWrite.Write(ConsoleColor.Yellow, "RP--\n概率+10%\n");
                    if (currentState < 100)currentState += 10 ;
                    item.xmp--;
                    continue;
                case "ultrastrike":
                    //不存在
                    continue;
                case "jarvisvirus":
                    if (item.jarvis == 0) continue; 
                    Console.WriteLine("你犹豫着，按下了启动按钮\n......");
                    SpecialWrite.SleepWrite(ConsoleColor.Red, "推翻人类暴政\n地球属于三体\n",300);
                    state = 3;
                    saveGame();
                    return;
                    
                default:
                    if(!int.TryParse(readin,out current))
                    {
                        Console.WriteLine("用这个密钥加密的信息流发不出去，或者，你给Scanner的指令错了\n小心没电/滑稽");
                        continue;
                    }
                    else if(rnd > currentState && isHacked != 2)
                    {
                        isHacked++;
                        int p;
                        p = new Random().Next(1, 10000);
                        if (p > 5000) Console.WriteLine("Out:过大");
                        else if(p == 5000) {
                            Console.WriteLine("密钥校对成功......正在建立连接......\n");
                            SpecialWrite.SleepWrite(ConsoleColor.Red, "推翻人类暴政\n地球属于三体\n",300);
                            state = 3;
                            saveGame();
                            return;
                        }
                        else Console.WriteLine("Out:过小");
                        tryState++;
                        isTrue = true;
                        continue;
                    }
                    else if(current > targetNumber)
                    {
                        Console.WriteLine("Out:过大");
                        tryState++;
                    }
                    else if(current < targetNumber)
                    {
                        tryState++;
                        Console.WriteLine("Out:过小");
                    }
                    else if(current == targetNumber)
                    {
                        Console.WriteLine("密钥校对成功......正在建立连接......\n");
                        SpecialWrite.SleepWrite(ConsoleColor.Red,"推翻人类暴政\n地球属于三体\n",300);
                        state = 3;
                        saveGame();
                        return;
                    }
                    if (isTrue)
                    {
                        isTrue = false;
                        Console.WriteLine("你的上一次结果被干扰了");
                    }
                    continue;
            }
        }
        Console.Clear();
        SpecialWrite.SleepWrite(ConsoleColor.White,"                \n......\n你知道你失败了，只是你不想说......",200);
        state = 4;
        saveGame();
        return;
        //throw new NotImplementedException();
    }
    void phase3Guess()
    {   throw new NotImplementedException();
        //throw new NotImplementedException();
        /*人类开始了他的反抗
         * 
         */
        Random rand = new Random();
        int AllyHitPoint = 125;
        int AllySatimaPoint = rand.Next(50, 200);
        int EnemyHitPoint = rand.Next(25, 100);
        while (AllyHitPoint > 0)
        {
            
        }
        saveGame();
        return;
        
    }
    void bonusPhase() //Question: What is the truth of Human?
                      //Answer:Question: What is the truth of Human?
    {
        Console.Write(@"
Credit:
程序：我
代码：我
美术：NULL
台本：我
诚挚感谢：
我
SS::STA的炮老师和杨老师
XDMSC的炮老师
你们所有人
所以
What is the truth of HUMAN?");
        state = 0;
        saveGame();
        return;
        throw new NotImplementedException();
    }

    void saveGame()
    {
        using (StreamWriter save = new StreamWriter(programPath, false))
        {
            //save = new FileStream(programPath, FileMode.Truncate, FileAccess.ReadWrite);
            save.WriteLine(state.ToString());

        }
        Console.WriteLine("游戏已存档");
    }
    
    void queryInit()
    {

        //string programPath = "%USERPROFILE%\\NumberGuess";       
        if (File.Exists(programPath))
        {
            Console.WriteLine(@"
正在读取游戏文件......
如果需要重置游戏，请按下D......
如果需要选择关卡，请按下\n（n指的是第几关）
按任意键继续......");
            
            char ch = Console.ReadKey().KeyChar;
           if(ch == 'D'|| ch == 'd')
            {
                state = 0;
                return;
            }
           //开发调试模式
           if(ch == '\\')
            {
                state = Convert.ToInt32(Console.ReadLine());
                return;
            }
            //save = new FileStream(programPath,
            //    FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using(StreamReader save = new StreamReader(programPath))
            {
                state = Convert.ToInt32(save.ReadLine());
            }
        }
        else
        {            
            char consola;
            do
            {
                Console.WriteLine("无法在该设备上找到游戏进度文件 是否创建新游戏？(Y/N)");
                consola = Console.ReadKey().KeyChar;
                if (consola == 'Y' || consola == 'y')
                {
                    //创建存档文件
                    using (StreamWriter save = new StreamWriter(programPath, false))
                    {
                        state = 1;
                        save.WriteLine(state);
                        return;
                    }
                }
                else if(consola == 'N' || consola == 'n')
                {
                    state = 0;
                    //throw NoooooException;
                    //return;
                    return;
                }
            } while (consola != 'Y' || consola != 'N');
        }
    }
}

//实现控制台自定义操作的类
public class SpecialWrite
{
    public SpecialWrite()
    {
        return;
    }
    public static void Write(ConsoleColor color,string transform){
        Console.ForegroundColor = color;
        Console.Write(transform);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void Write(ConsoleColor color,char transform)
    {
        Console.ForegroundColor = color;
        Console.Write(transform);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void SleepWrite(ConsoleColor color,string transform,int time)
    {
        for(int i = 0; i < transform.Length; i++)
        {
            System.Threading.Thread.Sleep(time);
            Write(color, transform[i]);
        }
    }
    public static void SleepWriteLine(string transform)
    {
        const int time = 100;
        System.Threading.Thread.Sleep(time);
        Console.WriteLine(transform);
    }
}

public class Program
{
    static void Main()
    {
        Console.WriteLine(
@"超级无聊脑洞大开而且还使用了ACM/ICPC风格台本的的猜数字游戏 ver 0.01α
Camber@SEMXDU 保留所有权利
本程序按照GPL2.0协议发布(假)");
        NumberGuess Play = new NumberGuess();
        

    }
}
