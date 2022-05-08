using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WpfComAp
{


    public class VCDGenerator
	{
		private static char[] chars = new char[] { '!', '\'', '#', '$', '%', '&', '\"', '(', ')', '*', '+', ',', '-', '.', '/' };

		public const int colTime = 1;
		public const int colID = 2;

		private static String[] varTypes = new String[] { "integer 16", "string 128", "integer 16", "string 128"};
		private static String[] varReferences = new String[] { "poradie", "data", "dataLength", "time" };
		private static int[] varColumns = new int[] { -1, 9, -1, -1 };
		
		private string[] files;
		private List<string> allIDs;
		private StreamWriter writer;
		List<String> allowedIds;
        public List<string> AllIDs { get => allIDs; set => allIDs=value; }

        public VCDGenerator(string[] files)
		{
			this.files = files;
			allIDs = new List<String>();
		}
		// TODO: zoradit súbory podla casu
		public async void Generate(string outputFile)
		{
			FindAllIDs();
			writer = new StreamWriter(outputFile);
			// writing header section
			Write("$date today $end");
			WriteLine();
			Write("$timescale 1 us $end");
			WriteLine();

			String[][] varIdentifiers = GenerateVarIdentifiers(allIDs.Count, VCDGenerator.varTypes.Length);
			String[][] varInitialisers = AssembleVarInit(allIDs, varIdentifiers);
			WriteDefinitions(allIDs, varInitialisers);

			// writing data section
			//Write("#" + startTime);

			// start time
			WriteLine();
			await WriteDataAsync(varIdentifiers, allIDs);
			//Console.WriteLine(vcdText);


		}
		public void setIdFilter(List<string> ids) 
		{ 
			allowedIds = ids;
		}


		
		public void FindAllIDs()
		{
			allIDs.Clear();
			if (allowedIds is null)
				for (int i = 0; i < files.Length; i++)
				{
					var file = files[i];

					StreamReader reader = new StreamReader(file, Encoding.UTF8);

					reader.ReadLine();
					//int j = 1;
					string line;
					while((line = reader.ReadLine()) != null)
					{
						
						string id = line.Split(";")[VCDGenerator.colID];
						if (!allIDs.Contains(id))
						{
							allIDs.Add(id);
						}
						/*if (i == 0 && j == 1)
						{
							startTime = row[VCDGenerator.colTime];
						}
						j++;
						*/
						

					}
				}
			else
			{
				foreach (string id in allowedIds)
					allIDs.Add(id);
			}
		}
		
		



		
		private string[][] AssembleVarInit(List<string> ids, String[][] varIdentifiers)
		{
			String[][] varInitialisers = new String[ids.Count][];
			for (var i = 0; i < ids.Count; i++)
			{
				varInitialisers[i] = new String[VCDGenerator.varTypes.Length];
				for (var j = 0; j < VCDGenerator.varTypes.Length; j++)
				{
					varInitialisers[i][j] = "$var " + VCDGenerator.varTypes[j] + " " + varIdentifiers[i][j] + " " + VCDGenerator.varReferences[j] + "_" + ids[i] + " $end";
				}
			}
			return varInitialisers;
		}


		private string[][] GenerateVarIdentifiers(int numIDs, int numVars)
		{
			String[][] varIdentifiers = new String[numIDs][];
			int charIterator = 0;
			for (var i = 0; i < numIDs; i++)
			{
				varIdentifiers[i] = new string[numVars];
				for (var j = 0; j < numVars; j++)
				{
					String identifier;
					if (charIterator < chars.Length-1)
					{
						identifier = Convert.ToString(chars[charIterator % chars.Length]);
					}
					else
					{
						identifier = new(chars[charIterator % chars.Length], charIterator / chars.Length+1);
					}
					charIterator++;
					varIdentifiers[i][j] = identifier;
				}
			}
			return varIdentifiers;
		}


		private void WriteDefinitions(List<string> ids, String[][] varInitialisers)
		{
			for (var i = 0; i < ids.Count; i++)
			{
				Write("$scope module " + ids[i] + " $end");
				WriteLine();
				for (var j = 0; j < varInitialisers[i].Length; j++)
				{
					Write(varInitialisers[i][j]);
					WriteLine();
				}
				Write("$upscope $end");
				WriteLine();
			}
			Write("$enddefinitions $end");
			WriteLine();
		}


		private async Task WriteDataAsync(String[][] varIdentifiers, List<string> ids)
		{
			var numIDs = varIdentifiers.Length;
			int index = 0;

			foreach (var file in files)
			{
				
				TextReader reader = new StreamReader(file, Encoding.UTF8);
				await reader.ReadLineAsync();

				int i = 0;
				while(true)
				{
					string line = reader.ReadLine();
					if (line == null)
						break;

					string[] dataRow = line.Split(";");
					if(allowedIds is not null && !allowedIds.Contains(dataRow[colID]))
                    {
						continue;
                    }
					Write("#" + dataRow[VCDGenerator.colTime] + " ");

					for (var j = 0; j < numIDs; j++)
					{
						if (dataRow[VCDGenerator.colID].Equals(ids[j]))
						{
							// ked ID riadka nezodpoveda id v DB tak sa preskoci
							WriteVCDRow(dataRow, varIdentifiers[j], index);
						}
						else if (index == 0)
						{
							String[] blankDataRow = new String[] {
								"" , dataRow[VCDGenerator.colTime] , "0" , "0" , "0" , "0" , "0" , "0" , "0" , "0" , "0"
							};
							WriteVCDRow(blankDataRow, varIdentifiers[j], -1);
						}
						i++;
					}
					index++;
				}	
			}
		}

		

		void WriteVCDRow(String[] dataRow, String[] varIdentifiers, int i)
			{
				var numVars = varIdentifiers.Length;
				for (var k = 0; k < numVars; k++)
				{
					if (VCDGenerator.varColumns[k] >= 0)
					{
						if (VCDGenerator.varTypes[k].Contains("integer"))
						{
							Write("b" + Convert.ToString(int.Parse(dataRow[VCDGenerator.varColumns[k]]), 2) + " " + varIdentifiers[k] + " ");
						}
						else if (VCDGenerator.varTypes[k].Contains("string"))
						{
							Write("s" + dataRow[VCDGenerator.varColumns[k]] + " " + varIdentifiers[k] + " ");
						}
					}
					else
					{
						if (VCDGenerator.varReferences[k].Equals("poradie"))
						{
							Write("b" + Convert.ToString(i + 1, 2) + " " + varIdentifiers[k] + " ");
						}
						else if (VCDGenerator.varReferences[k].Equals("dataLength"))
						{
							Write("b" + GetDataLength(int.Parse(dataRow[8])) + " " + varIdentifiers[k] + " ");
						}
						else if (VCDGenerator.varReferences[k].Equals("time"))
						{
						long timeTicks = (long.Parse(dataRow[colTime])*10)/* - (long.Parse(startTime)*10)*/;
						TimeSpan t = TimeSpan.FromTicks(timeTicks);

							string formattedTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms:{4:D3}us",
													t.Hours,
													t.Minutes,
													t.Seconds,
													t.Milliseconds,
													(t.Ticks/10)%1000);
							Write("s" + formattedTime + " " + varIdentifiers[k] + " ");
						}
					}
				}
				WriteLine();
			}
			String GetDataLength(int dlc)
			{
				int dataLength;
				if (dlc <= 8)
				{
					dataLength = dlc;
				}
				else
				{
					switch (dlc)
					{
						case 9:
							dataLength = 12;
							break;
						case 10:
							dataLength = 16;
							break;
						case 11:
							dataLength = 20;
							break;
						case 12:
							dataLength = 24;
							break;
						case 13:
							dataLength = 32;
							break;
						case 14:
							dataLength = 48;
							break;
						case 15:
							dataLength = 64;
							break;
						default:
							throw new Exception("Unexpected value: " + dlc.ToString());
					}
				}
				return Convert.ToString(dataLength, 2);
			}

		private void Write(string s)
        {
			writer.Write(s);
        }
		private void WriteLine()
		{
			writer.WriteLine();
		}

	}
}
