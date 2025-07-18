import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { useContext } from 'react';
import { Pie } from 'react-chartjs-2';
import { CryptoContext } from '../../context/crypto-context';

ChartJS.register(ArcElement, Tooltip, Legend);



export default function PortfolioChart() {
const {wallet, prices} = useContext(CryptoContext)

const data = {
  labels: wallet.map(c => c.name),
  datasets: [
    {
      label: '$',
      data: wallet.map(c => c.totalCost),
      backgroundColor: [
        'rgba(255, 99, 132, 1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)',
        'rgb(53, 110, 0)',
      ],
    },
  ],
};

    return(
        <div style={{display: 'flex', marginBottom: '1rem', justifyContent: 'center', height: '35%'}}>
            <Pie options={{ maintainAspectRatio: false }} data={data}></Pie>
        </div>
    )
}