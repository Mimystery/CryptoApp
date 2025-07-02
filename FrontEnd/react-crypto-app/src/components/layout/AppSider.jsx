import { Button, Flex, Layout } from 'antd';
import { ArrowDownOutlined, ArrowUpOutlined } from '@ant-design/icons';
import { useContext, useEffect, useState } from "react";
import { Card, Statistic, List, Typography} from 'antd';
import { percentDifference } from '../../utils'
import { CryptoContext } from '../../context/crypto-context';

const siderStyle = {
  textAlign: 'center',
  lineHeight: '120px',
  color: '#fff',
  backgroundColor: '#1677ff',
  padding: '0.5rem'
};

export default function AppSider(){
const {prices, wallet, loading, isAuthenticated} = useContext(CryptoContext)

const onClick = (coin) => {
  console.log(coin)
}

    return(
      <Layout.Sider width="15%" style={siderStyle}>

      {!isAuthenticated ? (
        <Card style={{ width: '100%', marginBottom: '1rem' }}>
        <Typography.Text strong>
          Login to check your profit
        </Typography.Text>
      </Card>) : (
        <>
      {wallet.length === 0 ? (
      <Card style={{ width: '100%', marginBottom: '1rem' }}>
        <Typography.Text strong>
          You dont have any coins yet
        </Typography.Text>
      </Card>
      ) : (
      wallet.map(coin => (
        <Card key={coin.symbol} style={{ width: '100%', marginBottom: '1rem' }}>
          <Statistic 
            title={coin.name}
            value={coin.growPercent}
            precision={2}
            prefix={coin.grow ? <ArrowUpOutlined /> : <ArrowDownOutlined />}
            valueStyle={coin.grow ? { color: '#3f8600' } : { color: '#cf1322' }}
            suffix="%"
          />
          <List
            size='small'
            dataSource={[
              {title: 'Total Profit', value: coin.totalProfit, isDollar: true},
              {title: 'Amount', value: coin.totalAmount},
              {title: 'Total Cost', value: coin.totalCost, isDollar: true},
            ]}
            renderItem={item => (
              <List.Item>
                <Typography.Text strong>{item.title}:</Typography.Text>
                {/* {(coin.value).toFixed(2)} */}
                
                {item.isDollar && (<Typography.Text type={coin.grow ? 'success' : 'danger'}>{(item.value?.toFixed(2)) ?? '0.00'}$</Typography.Text>)}
                {!item.isDollar && <span>{(item.value?.toFixed(4)) ?? '0.00'}</span>}
              </List.Item>
            )}/>
            <Button type="primary" color="danger" variant="solid" onClick={() => onClick(coin)}>Sell coin</Button>
        </Card>
        ))
      )}
        </>)}

      <Drawer
    width={'30%'}
    title="Sell coin"
    closable={{ 'aria-label': 'Close Button' }}
    open={true}
    padding={'0rem'}
    destroyOnHidden>
  </Drawer>

      </Layout.Sider>
    )
}