using System;

namespace HelloWorld
{
	class MainClass
	{
		static void Main (string[] args)
		{

			{
				//Собираюсь на пикник, хочу взять с собой термос чая. 
				//Cколько мне в него грамм сахара засыпать? 
				//В кружку я обычно три кубика кладу, но сейчас под рукой нет рафинада, 
				//а хочу, чтобы чай был такой же по сладости.
				Func<double,double,bool> EnoughSugar = (ThermosVolume, SugarMass) => {
					int PiecesOfSugarForCup = 3; //Количество кубиков рафинада на кружку
					double CupVolume = 0.3; //Объем кружки (л)
					double PieceOfSugarMass = 5; //Масса кубика рафинада (г)

					double SugarMassRequired; //Требуемая масса сахара в термосе (г)
					SugarMassRequired = ThermosVolume * PiecesOfSugarForCup * PieceOfSugarMass / CupVolume;

					if (SugarMassRequired > SugarMass)
						return false;
					else
						return true;
				};

				double UserThermosVolume = 1.5; //Объем термоса (л)
				double UserSugarMass = 55; //Масса имеющегося сахара (г)
				bool result = EnoughSugar (UserThermosVolume, UserSugarMass);
			}

			/*
			{
				Action sugar = () => {
					//Дано 
					double VTer = 0.5; //Объем термоса (л)
					int NCup = 3; //Количество кубиков рафинада на кружку

					double VCup = 0.3; //Объем кружки (л)
					double MRaf = 5; //Масса кубика рафинада (г)

					//Найти
					double MSug; //Масса сахара в термосе (г)

					//Решение
					MSug = VTer * NCup * MRaf / VCup;

					//Ответ
					double result = MSug;
				};
			}*/

			{
				//В двухмерной системе координат есть прямоугольная мишень, 
				//расположение и размеры которой заданы координатами противоположных углов.
				//И есть координаты места попадания выстрела. 
				//Нужно определить, пришлось ли попадание на мишень или нет.
				Func<double, double, double, double, double, double, bool> ShotInTarget = 
					(TargetX1,TargetY1,TargetX2,TargetY2,XShot,YShot) => {
					double TargetXMin = TargetX1;
					double TargetXMax = TargetX2;
					double TargetYMin = TargetY1;
					double TargetYMax = TargetY2;
					if (TargetX1>TargetX2){
						TargetXMax = TargetX1;
						TargetXMin = TargetX2;
					}
					if (TargetY1>TargetY2){
						TargetYMax = TargetY1;
						TargetYMin = TargetY2;
					}
					if ((XShot > TargetXMin) && (XShot < TargetXMax) && 
						(YShot > TargetYMin) && (YShot < TargetYMax)) 
						return true;
					else return false;
				};

				bool result = ShotInTarget (0, 2, -2, 4,- 1, 3);
			}

			/*Action HiWorld = () => {
				Console.WriteLine ("Hello World!");
				//Console.ReadKey ();
				int result = 0;
			};

			Action QuadEq = () => {
				//Дано: коэффициенты уравнения a*x^2+b*x+c=0
				double a, b, c; 
				a = 1;
				b = 2;
				c = 1;
				//Найти: вещественные корни уравнения с заданными коэффициентами
				short NumberOfRealRoots = 0;
				double root1 = 0;
				double root2 = 0;
				//Решение: Определяем, какая формула будет использоваться, 
				//в зависимости от типа уравнения
				if (a != 0) { //Решение: определение формулы для квадратного уравнения
					double d;
					d = b * b - 4 * a * c;
					if (d >= 0) { // Решение: определение формулы в зависимости от числа вещественных корней
						root1 = (-b + Math.Sqrt (d)) / a / 2;//вычисление
						root2 = (-b - Math.Sqrt (d)) / a / 2;//вычисление
						if (d == 0)
							NumberOfRealRoots = 1;
						else
							NumberOfRealRoots = 2;
				} 
				} else {//Решение: определение формулы для линейного уравнения
					if (b != 0) {
						root1 = -c / b;//Вычисление
						NumberOfRealRoots = 1;
					}
				}
				//Ответ
				double result = NumberOfRealRoots;
				double result1, result2;
				if (result == 1)
					result1 = root1;
				if (result == 2) {
					result1 = root1;
					result2 = root2;
				}
				
			};*/
			/*int iAct = 1;
			Action ComboAction = () => Console.WriteLine("Empty Action");
			ComboAction = () => {
				QuadEq ();
				sugar ();
				if (iAct == 1) {
					iAct = 0;
					ComboAction ();
				} else {
					HiWorld ();
				}
			};
			ComboAction ();
			ComboAction ();*/
		}
	}
}
