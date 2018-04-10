﻿const pathPlugin = require('path');
const webpack = require('webpack');

//env代表环境变量，如果传入env.production表示正式生产环境
module.exports = (env) => {
    //是否开发环境
    const isDev = !(env && env.prod);

    //获取路径
    function getPath(path) {
        return pathPlugin.join(__dirname, path);
    }

    //打包util脚本库
    return {
        entry: { util: [getPath("Typings/util/index.ts")] },
        output: {
            publicPath: 'dist/',
            path: getPath("wwwroot/dist"),
            filename: "[name].js",
            library: '[name]'
        },
        resolve: {
            extensions: ['.js', '.ts']
        },
        devtool: "source-map",
        module: {
            rules: [
                { test: /\.ts$/, use: ['awesome-typescript-loader?silent=true'] }
            ]
        },
        plugins: [
            new webpack.DllReferencePlugin({
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
            new webpack.DllPlugin({
                path: getPath("wwwroot/dist/[name]-manifest.json"),
                name: "[name]"
            }),
            new webpack.optimize.ModuleConcatenationPlugin()
        ].concat(isDev ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    }
}