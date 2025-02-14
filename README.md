# NinjaTrader_Data_Exporter
## Overview
`ChartData` is a **NinjaTrader 8** indicator that **exports chart data** to a CSV file, which can then be used for further analysis in **Excel, Python, or other data-processing tools**. This tool is useful for traders who want to **log historical price data**, conduct **backtesting**, or integrate chart data into **custom predictive models**.

### Features
- Automatically exports **Time, Open, High, Low, and Close** prices for each bar.
- Saves data in **CSV format**, making it easy to analyze in Excel, Python, or other tools.
- Efficient file handling with **thread-safe writing** using a `ReaderWriterLockSlim`.
- Deletes the existing data file at initialization to ensure a **fresh dataset** each time the indicator runs.

---

## Installation
### 1. Download or Clone the Repository
```sh
git clone https://github.com/YSFKDR/NinjaTrader-Excel-Exporter.git
```

### 2. Import the Indicator into NinjaTrader 8
- Open **NinjaTrader 8**.
- Go to **Tools** > **Import** > **NinjaScript Add-On**.
- Select the `.cs` file (`ChartData.cs`) and import it.

### 3. Enable the Indicator
- Open a chart in NinjaTrader.
- Go to **Indicators** (Ctrl + I).
- Search for `ChartData` and add it to the chart.
- Click **Apply** and **OK**.

---

## Usage
### 1. Automatic Data Export
- Once the indicator is added to a chart, it will start logging data automatically.
- A CSV file named **`chartdata.csv`** is created in:
  ```
  D:\chartdata.csv
  ```
  *Note: You can modify this path in the source code if needed.*

### 2. Data Format
- The CSV file is structured as:
  ```csv
  Time,Open,High,Low,Close
  2024-02-14 09:30:00,4200.25,4201.50,4198.75,4200.00
  2024-02-14 09:35:00,4200.00,4203.00,4199.50,4202.25
  ```
- This format allows for **easy import** into Excel, Pandas (Python), or other data-processing tools.

### 3. Using the Data in Excel
- Open the **CSV file** in Excel.
- Use **Data → Text to Columns** (Delimited by comma) for better readability.
- Perform **charting, trend analysis, or backtesting**.

### 4. Using the Data in Python (Example)
```python
import pandas as pd

df = pd.read_csv("D:\chartdata.csv")
print(df.head())  # Display first 5 rows
```

---

## Example Output
### Sample `chartdata.csv` File
```csv
Time,Open,High,Low,Close
2024-02-14 09:30:00,4200.25,4201.50,4198.75,4200.00
2024-02-14 09:35:00,4200.00,4203.00,4199.50,4202.25
2024-02-14 09:40:00,4202.25,4204.75,4201.00,4203.50
```

---

## Customization
### 1. Changing the File Path
Modify this line in `ChartData.cs` to save the file elsewhere:
```csharp
private string chartdata = @"D:\chartdata.csv";
```

### 2. Adjusting Data Frequency
Change the update frequency by modifying:
```csharp
Calculate = Calculate.OnBarClose;  // Exports data on bar close
```
Use `Calculate.OnEachTick` for tick-based exports. **Note: Exporting data on each tick or on price change can generate a very large dataset, so plan accordingly based on your system’s storage and processing capabilities.**

---

## License
This project is licensed under the **MIT License** – free to use and modify.
