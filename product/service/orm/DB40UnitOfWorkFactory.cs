﻿using gorilla.utility;

namespace solidware.financials.service.orm
{
    public class DB40UnitOfWorkFactory : UnitOfWorkFactory
    {
        readonly ConnectionFactory factory;
        readonly Context context;

        public DB40UnitOfWorkFactory(ConnectionFactory factory, Context context)
        {
            this.factory = factory;
            this.context = context;
        }

        public UnitOfWork create()
        {
            if( context.contains(new TypedKey<Connection>()))
                return new EmptyUnitOfWork();
            var connection = factory.Open();
            context.add(new TypedKey<Connection>(), connection);
            return new DB40UnitOfWork(connection);
        }
    }
}